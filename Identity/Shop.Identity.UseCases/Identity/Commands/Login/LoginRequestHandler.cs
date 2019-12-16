using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shop.Identity.Entities;
using Shop.Utils.Exceptions;

namespace Shop.Identity.UseCases.Identity.Commands.Login
{
    internal class LoginRequestHandler : AsyncRequestHandler<LoginRequest>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginRequestHandler(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        protected override async Task Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.LoginDto.Email);
            if (user == null) throw new EntityNotFoundException();

            if (!await _userManager.IsEmailConfirmedAsync(user)) throw new InvalidOperationException("Email not confirmed");

            var result = await _signInManager.PasswordSignInAsync(request.LoginDto.Email, request.LoginDto.Password, true, lockoutOnFailure: false);

            if (result.IsLockedOut)
            {
                throw new InvalidOperationException("User locked out");
            }

            if (!result.Succeeded) throw new InvalidOperationException("Login failed");
        }
    }
}
