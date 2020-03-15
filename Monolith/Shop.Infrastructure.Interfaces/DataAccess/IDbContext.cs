using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Entities;

namespace Shop.Infrastructure.Interfaces.DataAccess
{
    public interface IDbContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Email> Emails { get; set; }
        Task<int> SaveChangesAsync(CancellationToken token = default);
        int SaveChanges();
    }
}
