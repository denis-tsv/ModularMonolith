using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Communication.Entities;

namespace Shop.Communication.Infrastructure.Interfaces.DataAccess
{
    internal interface ICommunicationDbContext
    {
        DbSet<Email> Emails { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
