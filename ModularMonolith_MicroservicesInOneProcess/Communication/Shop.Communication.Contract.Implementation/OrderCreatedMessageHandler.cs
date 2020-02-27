﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shop.Communication.Contract.Messages;
using Shop.Communication.Infrastructure.Interfaces.Services;
using Shop.Framework.Interfaces.Messaging;
using Shop.Order.Contract.Orders.Messages.CreateOrder;

namespace Shop.Communication.Contract.Implementation
{
    internal class OrderCreatedMessageHandler : INotificationHandler<OrderCreatedMessage>
    {
        private readonly IEmailService _emailService;
        private readonly IMessageBroker _messageBroker;

        public OrderCreatedMessageHandler(IEmailService emailService, IMessageBroker messageBroker)
        {
            _emailService = emailService;
            _messageBroker = messageBroker;
        }
        public async Task Handle(OrderCreatedMessage message, CancellationToken cancellationToken)
        {
            try
            {
                await _emailService.SendEmailAsync(message.UserEmail, "Order created", $"Your order {message.OrderId} created successfully");

                await _messageBroker.PublishAsync(new EntityEmailMessage
                {
                    Id = message.OrderId,
                    CorrelationId = message.CorrelationId
                });
            }
            catch (Exception e)
            {
                // log exception using correlation id

                await _messageBroker.PublishAsync(new ExceptionMessage
                {
                    CorrelationId = message.CorrelationId,
                    Exception = e
                });
            }
        }
    }
}