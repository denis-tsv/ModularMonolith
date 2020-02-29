using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Communication.Contract.Messages;
using Shop.Framework.Interfaces.Messaging;
using Shop.Order.Contract.Orders.Dto;
using Shop.Order.Contract.Orders.Messages.CreateOrder;
using Shop.Order.Contract.Orders.Messages.GetOrder;

namespace Shop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMessageDispatcher _messageDispatcher;

        public OrdersController(IMessageDispatcher messageDispatcher)
        {
            _messageDispatcher = messageDispatcher;
        }

        // GET api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            // we can create some service which will create messages, add CorrelationId and return messages result
            var message = new GetOrderMessage {Id = id, CorrelationId = Guid.NewGuid().ToString()};
            var resultMessage =  await _messageDispatcher.SendMessageAsync<OrderDetailsMessage>(message);
            return resultMessage.Order;
        }

        // POST api/orders
        [HttpPost]
        public async Task<int> Post([FromBody] CreateOrderDto createOrderDto)
        {
            var message = new CreateOrderMessage {CreateOrderDto = createOrderDto, CorrelationId = Guid.NewGuid().ToString() };
            var resultMessage = await _messageDispatcher.SendMessageAsync<UserEmailNotifiedMessage>(message);
            return resultMessage.OrderId;
        }
    }
}
