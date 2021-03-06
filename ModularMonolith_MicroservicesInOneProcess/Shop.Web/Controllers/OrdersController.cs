﻿using System.Linq;
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
            var message = new GetOrderRequestMessage {Id = id};
            var resultMessage =  await _messageDispatcher.SendMessageAsync<GetOrderResponseMessage>(message);
            return resultMessage.Order;
        }

        // POST api/orders
        [HttpPost]
        public async Task<int> Post([FromBody] CreateOrderDto createOrderDto)
        {
            var message = new CreateOrderRequestMessage { CreateOrderDto = createOrderDto };
            var resultMessageTypes = new[] { typeof(CreateOrderResponseMessage), typeof(UserEmailNotifiedMessage) };
            var resultMessages = await _messageDispatcher.SendMessageAsync(message, resultMessageTypes);
            return resultMessages.OfType<CreateOrderResponseMessage>().Single().OrderId;
        }
    }
}
