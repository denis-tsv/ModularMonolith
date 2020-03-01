using Shop.Framework.Interfaces.Messaging;

namespace Shop.Identity.Contract.Identity.UserDetails
{
    //We don't create message with result message type (for example, UserDetailsRequestMessage : ResultMessage<UserDetailsResponseMessage>)
    //because request and result messages can be in different modules and in may cause a circular dependency between module contracts
    public class UserDetailsRequestMessage : Message
    {
        
    }
}
