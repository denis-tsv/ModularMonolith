﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Order.UseCases.Orders.Commands.CreateOrder;
using Shop.Order.UseCases.Orders.Dto;
using Shop.Order.UseCases.Orders.Queries.GetOrder;

namespace Shop.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    internal class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id, CancellationToken token)
        {
            return await _mediator.Send(new GetOrderRequest { Id = id }, token);
        }

        // POST api/orders
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateOrderDto createOrderDto, CancellationToken token)
        {
            return await _mediator.Send(new CreateOrderRequest { CreateOrderDto = createOrderDto }, token);
        }
    }
}
