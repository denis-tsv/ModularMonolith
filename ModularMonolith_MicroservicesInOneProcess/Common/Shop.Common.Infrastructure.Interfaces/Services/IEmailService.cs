﻿using System.Threading.Tasks;

namespace Shop.Common.Infrastructure.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
