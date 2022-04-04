using Appccelerate.StateMachine;
using Appccelerate.StateMachine.Machine;
using MediatR;
using Shop.Communication.UseCases.Emails.Commands.ScheduleEmail;
using Shop.Framework.UseCases.Interfaces.Services;
using Shop.Order.UseCases.Orders.Commands.CreateOrder;
using Shop.Order.UseCases.Orders.Commands.DeleteOrder;
using Shop.Order.UseCases.Orders.Dto;

namespace Shop.Web.Sagas
{
    internal class CreateOrderSaga
    {
        private readonly ISender _sender;
        private readonly ICurrentUserService _currentUserService;
        private readonly PassiveStateMachine<States, Events> _machine;
        private CreateOrderDto _dto;
        private int _orderId;
        private bool _completed;

        private enum States
        {
            Initial,
            OrderCreated,
            EmailSend,
            Completed,
            Failed
        }

        private enum Events
        {
            CreateOrder,
            SendEmail,
            Completed,
            Failed
        }

        public CreateOrderSaga(ISender sender, ICurrentUserService currentUserService)
        {
            _sender = sender;
            _currentUserService = currentUserService;

            var builder = new StateMachineDefinitionBuilder<States, Events>();

            builder
                .In(States.Initial)
                .On(Events.CreateOrder)
                .Goto(States.OrderCreated)
                .Execute(OnCreateOrder);

            builder
                .In(States.OrderCreated)
                .On(Events.SendEmail)
                .Goto(States.EmailSend)
                .Execute(OnSendEmail);

            builder
                .In(States.EmailSend)
                .On(Events.Completed)
                .Goto(States.Completed)
                .Execute(OnCompleted);

            builder
                .In(States.OrderCreated)
                .On(Events.Failed)
                .Goto(States.Failed);

            builder
                .In(States.EmailSend)
                .On(Events.Failed)
                .Goto(States.Failed)
                .Execute(DeleteOrder);

            builder
                .WithInitialState(States.Initial);

            _machine = builder
                .Build()
                .CreatePassiveStateMachine();

            _machine.Start();
        }

        public void Start(CreateOrderDto dto)
        {
            _dto = dto;
            _machine.Fire(Events.CreateOrder);
        }

        public int? GetResult()
        {
            if (_completed) return _orderId;

            return null;
        }

        private void OnCreateOrder()
        {
            try
            {
                _orderId = _sender.Send(new CreateOrderRequest { CreateOrderDto = _dto }).GetAwaiter().GetResult();
                _machine.Fire(Events.SendEmail);
            }
            catch
            {
                _machine.Fire(Events.Failed);
            }
        }

        private void OnSendEmail()
        {
            try
            {
                var scheduleEmailCommand = new ScheduleEmailCommand
                {
                    OrderId = _orderId,
                    UserId = _currentUserService.Id,
                    Address = _currentUserService.Email,
                    Subject = "Order created",
                    Body = $"Your order {_orderId} created successfully"
                };
                _sender.Send(scheduleEmailCommand).Wait();
                _machine.Fire(Events.Completed);
            }
            catch
            {
                _machine.Fire(Events.Failed);
            }
        }

        private void DeleteOrder()
        {
            _sender.Send(new DeleteOrderCommand { Id = _orderId }).Wait();
        }

        private void OnCompleted()
        {
            _completed = true;
        }
    }
}
