using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Shop.Order.Infrastructure.Interfaces.DataAccess
{
    internal interface IOrderDbContext
    {
        DbSet<Entities.Order> Orders { get; set; }

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
