﻿using MediatR;
using Shop.Order.Contract.Dto;

namespace Shop.Order.UseCases.Orders.Commands.CreateOrder
{
    internal class CreateOrderRequest : IRequest
    {
        public CreateOrderDto CreateOrderDto { get; set; }
    }
}
