using System;

namespace Shop.Framework.Interfaces.Messaging
{
    public abstract class ErrorMessage : Message
    {
        public Exception Exception { get; set; }
    }
}
