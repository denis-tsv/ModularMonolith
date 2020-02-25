﻿using System.Threading.Tasks;
using Shop.Utils.Messaging;

namespace Shop.Web.Utils.WaitingTasksStore
{
    public interface IWaitingTasksStore
    {
        Task<TMessage> Add<TMessage>(string correlationId) where TMessage : Message;
        bool TryComplete<TMessage>(TMessage message) where TMessage : Message;
    }
}
