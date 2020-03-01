using Shop.Framework.Interfaces.Messaging;
using Shop.Identity.Contract.Identity.Dto;

namespace Shop.Identity.Contract.Identity.Login
{
    public class LoginRequestMessage : Message
    {
        public LoginDto LoginDto { get; set; }
    }
}
