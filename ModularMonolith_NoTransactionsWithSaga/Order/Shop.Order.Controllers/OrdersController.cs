using System;
using System.Threading.Tasks;
using Appccelerate.StateMachine;
using Appccelerate.StateMachine.Machine;
using Microsoft.AspNetCore.Mvc;
using Shop.Communication.Contract;
using Shop.Framework.Interfaces.Services;
using Shop.Order.Contract;
using Shop.Order.Contract.Dto;

namespace Shop.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderContract _orderContract;

        public OrdersController(IOrderContract orderContract)
        {
            _orderContract = orderContract;
        }

        // GET api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            return await _orderContract.GetOrderAsync(id);
        }

        // POST api/orders
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateOrderDto createOrderDto, [FromServices] CreateOrderStateMachine stateMachine)
        {
            stateMachine.Start(createOrderDto);
            var orderId = stateMachine.GetResult();
            if (orderId == null) throw new Exception("Unable to create order");
            return orderId;
        }
    }

    public class CreateOrderStateMachine
    {
        private readonly IOrderContract _orderContract;
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

        public CreateOrderStateMachine(
            IOrderContract orderContract, 
            ICommunicationContract communicationContract,
            ICurrentUserService currentUserService)
        {
            _orderContract = orderContract;
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
                _orderId = _orderContract.CreateOrderAsync(_dto).GetAwaiter().GetResult();
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
                _communicationContract.SendEmailAsync(_currentUserService.Email, "Order Created", "Order Created", _orderId).GetAwaiter().GetResult();
                _machine.Fire(Events.Completed);
            }
            catch 
            {
                _machine.Fire(Events.Failed);
            }
        }

        private void DeleteOrder()
        {
            _orderContract.DeleteOrderAsync(_orderId).GetAwaiter().GetResult();
        }

        private void OnCompleted()
        {
            _completed = true;
        }
    }
}
