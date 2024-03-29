﻿using System;
using System.Linq;
using FluentScheduler;
using Shop.Communication.DataAccess.Interfaces;
using Shop.Emails.Interfaces;

namespace Shop.Communication.BackgroundJobs.BackgroundJobs
{
    internal class SendEmailsJob : IJob
    {
        private readonly ICommunicationDbContext _dbContext;
        private readonly IEmailService _emailService;

        public SendEmailsJob(ICommunicationDbContext dbContext, IEmailService emailService)
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