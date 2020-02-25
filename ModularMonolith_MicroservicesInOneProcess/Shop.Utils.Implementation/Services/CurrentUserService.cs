﻿using Microsoft.AspNetCore.Http;
using Shop.Utils.Services;

namespace Shop.Utils.Implementation.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            IsAuthenticated = httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
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
