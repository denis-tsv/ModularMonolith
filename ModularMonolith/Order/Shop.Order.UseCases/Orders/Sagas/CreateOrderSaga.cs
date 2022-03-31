using System.Threading;
using Appccelerate.StateMachine;
using Appccelerate.StateMachine.Machine;
using MediatR;
using Shop.Communication.Contract;
using Shop.Framework.UseCases.Interfaces.Services;
using Shop.Order.DataAccess.Interfaces;
using Shop.Order.UseCases.Orders.Commands.CreateOrder;
using Shop.Order.UseCases.Orders.Dto;

namespace Shop.Order.UseCases.Orders.Sagas;

internal class CreateOrderSaga
{
    private readonly ISender _sender;
    private readonly IOrderDbContext _dbContext;
    private readonly ICommunicationContract _communicationContract;
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
        Complete,
        Failed
    }

    private enum Events
    {
        CreateOrder,
        SendEmail,
        Completed,
        Failed
    }

    public CreateOrderSaga(
        ISender sender,
        IOrderDbContext dbContext,
        ICommunicationContract communicationContract,
        ICurrentUserService currentUserService)
    {
        _sender = sender;
        _dbContext = dbContext;
        _communicationContract = communicationContract;
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
            .Goto(States.Complete)
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
            _orderId = _sender.Send(new CreateOrderRequest { CreateOrderDto = _dto}).GetAwaiter().GetResult();
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
            _communicationContract.ScheduleOrderCreatedEmailAsync(_currentUserService.Email, _orderId, CancellationToken.None).GetAwaiter().GetResult();
            _machine.Fire(Events.Completed);
        }
        catch
        {
            _machine.Fire(Events.Failed);
        }
    }

    private void DeleteOrder()
    {
        var order = _dbContext.Orders.Find(_orderId);
        _dbContext.Orders.Remove(order);
        _dbContext.SaveChangesAsync().Wait();
    }

    private void OnCompleted()
    {
        _completed = true;
    }
}