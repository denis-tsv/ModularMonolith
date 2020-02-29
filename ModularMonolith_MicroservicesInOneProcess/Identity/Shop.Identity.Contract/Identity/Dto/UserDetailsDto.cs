using System;

namespace Shop.Identity.Contract.Identity.Dto
{
    public class UserDetailsDto
    {
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
