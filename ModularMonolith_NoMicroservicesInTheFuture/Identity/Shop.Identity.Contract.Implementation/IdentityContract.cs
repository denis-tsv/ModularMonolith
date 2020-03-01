using System.Threading.Tasks;
using MediatR;
using Shop.Identity.UseCases.Identity.Commands.Login;
using Shop.Identity.UseCases.Identity.Dto;

namespace Shop.Identity.Contract.Implementation
{
    internal class IdentityContract : IIdentityContract
    {
        private readonly IMediator _mediator;

        public IdentityContract(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task LoginAsync(LoginDto dto)
        {
            await _mediator.Send(new LoginRequest { LoginDto = dto });
        }
    }
}
