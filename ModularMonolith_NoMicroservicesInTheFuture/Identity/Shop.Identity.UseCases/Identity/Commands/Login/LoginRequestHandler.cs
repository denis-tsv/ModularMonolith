using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Framework.Interfaces.Exceptions;
using Shop.Identity.Infrastructure.Interfaces.DataAccess;

namespace Shop.Identity.UseCases.Identity.Commands.Login
{
    internal class LoginRequestHandler : AsyncRequestHandler<LoginRequest>
    {
        private readonly IIdentityDbContext _dbContext;

        public LoginRequestHandler(IIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.AsNoTracking()
                .SingleOrDefaultAsync(x => x.NormalizedEmail == request.LoginDto.Email.ToUpper(), cancellationToken: cancellationToken);
            if (user == null) throw new EntityNotFoundException();
        }
    }
}
