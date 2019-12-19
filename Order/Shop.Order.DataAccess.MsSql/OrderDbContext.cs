using Microsoft.EntityFrameworkCore;
using Shop.Order.Entities;
using Shop.Order.Infrastructure.Interfaces.DataAccess;

namespace Shop.Order.DataAccess.MsSql
{
    internal class OrderDbContext : DbContext, IOrderDbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {            
        }
                
        public DbSet<Entities.Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Order");
        }
    }
}
