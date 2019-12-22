using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Identity.UseCases.Identity.Commands.Login;
using Shop.Identity.UseCases.Identity.Dto;

namespace Shop.Identity.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task Login([FromBody]LoginDto loginDto)
        {
            await _mediator.Send(new LoginRequest {LoginDto = loginDto});
        }

        //Register

        //Logout

        //Forgot password

        //Confirm email
    }
}
