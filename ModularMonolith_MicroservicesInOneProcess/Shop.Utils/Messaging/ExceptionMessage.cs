using System;

namespace Shop.Utils.Messaging
{
    public class ExceptionMessage : Message
    {
        public Exception Exception { get; set; }
    }
}
