using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Communication.DataAccess.Interfaces;
using Shop.Communication.Entities;

namespace Shop.Communication.UseCases.Emails.Commands.ScheduleEmail
{
    internal class ScheduleEmailCommandHandler : AsyncRequestHandler<ScheduleEmailCommand>
    {
        private readonly ICommunicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ScheduleEmailCommandHandler(ICommunicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        protected override async Task Handle(ScheduleEmailCommand request, CancellationToken cancellationToken)
        {
            var email = _mapper.Map<Email>(request);

            _dbContext.Emails.Add(email);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}