using MediatR;
using Shop.UseCases.Identity.Dto;

namespace Shop.UseCases.Identity.Commands.Login
{
    public class LoginRequest : IRequest
    {
        public LoginDto LoginDto { get; set; }
    }
}
