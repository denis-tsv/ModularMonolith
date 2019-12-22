using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Common.Entities;

namespace Shop.Common.Infrastructure.Interfaces.DataAccess
{
    internal interface ICommonDbContext
    {
        DbSet<Email> Emails { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
