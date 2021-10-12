using Microsoft.EntityFrameworkCore;
using Shop.Communication.DataAccess.Interfaces;
using Shop.Communication.Entities;

namespace Shop.Communication.DataAccess.MsSql
{
    internal class CommunicationDbContext : DbContext, ICommunicationDbContext
    {
        public CommunicationDbContext(DbContextOptions<CommunicationDbContext> options) : base(options)
        {
        }

        public DbSet<Email> Emails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Communication");
        }
    }
}
