using Shop.Framework.Interfaces.Messaging;
using Shop.Identity.Contract.Identity.Dto;

namespace Shop.Identity.Contract.Identity.UserDetails
{
    public class UserDetailsResponseMessage : Message
    {
        public UserDetailsDto UserDetailsDto { get; set; }
    }
}
