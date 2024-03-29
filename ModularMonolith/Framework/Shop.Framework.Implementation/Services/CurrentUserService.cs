﻿using Microsoft.AspNetCore.Http;
using Shop.Framework.UseCases.Interfaces.Services;

namespace Shop.Framework.UseCases.Implementation.Services
{
    internal class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            IsAuthenticated = httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true;
            Id = 1;
            Email = "test@tets.com";

            //if (IsAuthenticated)
            //{
            //    UserId = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            //    Email = _httpContextAccessor.HttpContext.User.Identity.Name;
            //}
        }

        public int Id { get; }
        public bool IsAuthenticated { get; }
        public string Email { get; set; }
    }
}
