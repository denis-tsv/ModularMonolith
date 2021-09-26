using System;
using Microsoft.AspNetCore.Http;
using Shop.Framework.Interfaces.Services;

namespace Shop.Framework.Implementation.Services
{
    internal class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            IsAuthenticated = httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            Id = 1;
            Email = "test@tets.com";
            CorrelationId = Guid.NewGuid().ToString("N");


            //if (IsAuthenticated)
            //{
            //    UserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            //    Email = _httpContextAccessor.HttpContext.User.Identity.Name;
            //}
        }

        public int Id { get; }
        public bool IsAuthenticated { get; }
        public string Email { get; }

        public string CorrelationId { get; }
    }
}
