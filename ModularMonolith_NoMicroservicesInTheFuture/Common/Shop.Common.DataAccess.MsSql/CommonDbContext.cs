using Microsoft.EntityFrameworkCore;
using Shop.Common.Entities;
using Shop.Common.Infrastructure.Interfaces.DataAccess;

namespace Shop.Common.DataAccess.MsSql
{
    internal class CommonDbContext : DbContext, ICommonDbContext
    {
        public CommonDbContext(DbContextOptions<CommonDbContext> options) : base(options)
        {
        }

        public DbSet<Email> Emails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Common");
        }
    }
}
