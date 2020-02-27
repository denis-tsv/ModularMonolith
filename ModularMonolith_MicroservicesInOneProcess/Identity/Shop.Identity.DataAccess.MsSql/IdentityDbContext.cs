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

            DataSeed(modelBuilder);
        }

        private void DataSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "test@tets.com",
                    EmailConfirmed = true,
                }
            );
        }
    }
}
