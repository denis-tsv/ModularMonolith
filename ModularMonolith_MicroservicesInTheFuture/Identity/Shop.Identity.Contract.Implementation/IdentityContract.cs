using System.Threading.Tasks;
using MediatR;
using Shop.Identity.Contract.Dto;
using Shop.Identity.UseCases.Identity.Commands.Login;

namespace Shop.Identity.Contract.Implementation
{
    internal class IdentityContract : IIdentityContract
    {
        private readonly IMediator _mediator;

        public IdentityContract(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task LoginAsync(LoginDto loginDto)
        {
            await _mediator.Send(new LoginRequest { LoginDto = loginDto });
        }
    }
}
