using System.Threading;
using System.Threading.Tasks;
using Appccelerate.StateMachine;
using Appccelerate.StateMachine.AsyncMachine;
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
        private readonly AsyncPassiveStateMachine<States, Events> _machine;
        private CreateOrderDto _dto;
        private CancellationToken _cancellationToken;
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
                .Execute(CreateOrderAsync);

            builder
                .In(States.OrderCreated)
                .On(Events.SendEmail)
                .Goto(States.EmailSend)
                .Execute(SendEmailAsync);

            builder
                .In(States.EmailSend)
                .On(Events.Completed)
                .Goto(States.Completed)
                .Execute(Complete);

            builder
                .In(States.OrderCreated)
                .On(Events.Failed)
                .Goto(States.Failed);

            builder
                .In(States.EmailSend)
                .On(Events.Failed)
                .Goto(States.Failed)
                .Execute(DeleteOrderAsync);

            builder
                .WithInitialState(States.Initial);

            _machine = builder
                .Build()
                .CreatePassiveStateMachine();
        }

        public async Task<int?> RunAsync(CreateOrderDto dto, CancellationToken cancellationToken)
        {
            _dto = dto;
            _cancellationToken = cancellationToken;

            await _machine.Start();
            await _machine.Fire(Events.CreateOrder);

            return _completed ? _orderId : null;
        }

        private async Task CreateOrderAsync()
        {
            try
            {
                _orderId = await _sender.Send(new CreateOrderRequest { CreateOrderDto = _dto }, _cancellationToken);
                await _machine.Fire(Events.SendEmail);
            }
            catch
            {
                await _machine.Fire(Events.Failed);
            }
        }

        private async Task SendEmailAsync()
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
                await _sender.Send(scheduleEmailCommand, _cancellationToken);
                await _machine.Fire(Events.Completed);
            }
            catch
            {
                await _machine.Fire(Events.Failed);
            }
        }

        private async Task DeleteOrderAsync()
        {
            await _sender.Send(new DeleteOrderCommand { Id = _orderId }, _cancellationToken);
        }

        private void Complete()
        {
            _completed = true;
        }
    }
}
