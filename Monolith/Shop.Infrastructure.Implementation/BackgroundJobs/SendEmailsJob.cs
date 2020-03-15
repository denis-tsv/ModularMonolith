using System;
using System.Linq;
using FluentScheduler;
using Shop.Infrastructure.Interfaces.DataAccess;
using Shop.Infrastructure.Interfaces.Services;

namespace Shop.Infrastructure.Implementation.BackgroundJobs
{
    public class SendEmailsJob : IJob
    {
        private readonly IDbContext _dbContext;
        private readonly IEmailService _emailService;


        public SendEmailsJob(IDbContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public void Execute()
        {
            foreach (var email in _dbContext.Emails.Where(x => !x.IsSended && x.Attempts < 3))
            {
                try
                {
                    _emailService.SendEmailAsync(email.Address, email.Subject, email.Body).Wait();

                    email.IsSended = true;
                }
                catch (Exception ex)
                {
                    //logging

                    email.Attempts++;
                }
            }

            _dbContext.SaveChanges();
        }
    }
}
