using System.Threading.Tasks;
using MediatR;
using Shop.Identity.Contract.Identity;
using Shop.Identity.Contract.Identity.Dto;
using Shop.Identity.UseCases.Identity.Commands.Login;

namespace Shop.Identity.Contract.Implementation
{
    internal class IdentityService : IIdentityService
    {
        private readonly IMediator _mediator;

        public IdentityService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task LoginAsync(LoginDto loginDto)
        {
            await _mediator.Send(new LoginRequest { LoginDto = loginDto });
        }
    }
}
