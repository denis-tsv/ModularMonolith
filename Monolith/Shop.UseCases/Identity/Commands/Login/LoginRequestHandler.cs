using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Interfaces.DataAccess;
using Shop.Utils.Exceptions;

namespace Shop.UseCases.Identity.Commands.Login
{
    public class LoginRequestHandler : AsyncRequestHandler<LoginRequest>
    {
        private readonly IDbContext _dbContext;

        public LoginRequestHandler(IDbContext dbContext)
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
