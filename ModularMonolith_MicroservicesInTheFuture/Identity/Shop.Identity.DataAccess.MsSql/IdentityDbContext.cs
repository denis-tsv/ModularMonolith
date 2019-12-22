using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Identity.Entities;
using Shop.Identity.Infrastructure.Interfaces.DataAccess;

namespace Shop.Identity.DataAccess.MsSql
{
    internal class IdentityDbContext : IdentityDbContext<User, Role, int>, IIdentityDbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.HasDefaultSchema("Identity");
        }
    }
}
