using System;

namespace Shop.Framework.Interfaces.Messaging
{
    public class ExceptionMessage : Message
    {
        public Exception Exception { get; set; }
    }
}
