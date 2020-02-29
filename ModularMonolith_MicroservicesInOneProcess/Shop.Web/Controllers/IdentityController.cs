using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Framework.Interfaces.Messaging;
using Shop.Identity.Contract.Identity.Dto;
using Shop.Identity.Contract.Identity.Login;

namespace Shop.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMessageDispatcher _messageDispatcher;

        public IdentityController(IMessageDispatcher messageDispatcher)
        {
            _messageDispatcher = messageDispatcher;
        }

        [HttpPost]
        public async Task Login([FromBody]LoginDto loginDto)
        {
            var message = new LoginMessage {LoginDto = loginDto};
            await _messageDispatcher.SendMessageAsync<LoginSucceededMessage>(message);
        }
        
    }
}
