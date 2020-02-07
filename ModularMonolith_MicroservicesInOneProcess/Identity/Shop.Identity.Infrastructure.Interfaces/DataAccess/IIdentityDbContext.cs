using Microsoft.EntityFrameworkCore;
using Shop.Identity.Entities;

namespace Shop.Identity.Infrastructure.Interfaces.DataAccess
{
    internal interface IIdentityDbContext
    {
        DbSet<User> Users { get; set; }
    }
}
