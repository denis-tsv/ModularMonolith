using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Communication.DataAccess.Interfaces;
using Shop.Communication.UseCases.Emails.Dto;

namespace Shop.Communication.UseCases.Emails.Queries.GetEmails
{
    internal class SendEmailRequestHandler : IRequestHandler<GetEmailsRequest, EmailDto[]>
    {
        private readonly ICommunicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SendEmailRequestHandler(ICommunicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public Task<EmailDto[]> Handle(GetEmailsRequest request, CancellationToken cancellationToken)
        {
            return _dbContext.Emails
                .ProjectTo<EmailDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);
        }
    }
}
