using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Framework.Interfaces.Messaging
{
    public interface IMessageStore
    {
        Task AddAsync<TMessage>(TMessage message) where TMessage : Message;
        Task<List<TMessage>> AllAsync<TMessage>(Guid correlationId) where TMessage : Message;
        Task<List<Message>> AllAsync(Guid correlationId);
        Task<TMessage> SingleOrDefaultAsync<TMessage>(Guid correlationId) where TMessage : Message;
    }
}
