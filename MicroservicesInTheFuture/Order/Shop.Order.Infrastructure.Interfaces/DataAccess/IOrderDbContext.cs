using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Order.Entities;

namespace Shop.Order.Infrastructure.Interfaces.DataAccess
{
    internal interface IOrderDbContext
    {
        DbSet<Entities.Order> Orders { get; set; }

        DbSet<Product> Products { get; set; }

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
