using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TattooHub.Domain.Entities;

namespace TattooHub.Application.Interfaces.Persistence
{
    //Patron Unidad de trabajo para transacciones
    public interface IUnitOfWork : IDisposable
    {
        IArtistRepository Artist { get; }
        IRepository<PortfolioItem> PortfolioItem { get; }
        IRepository<Domain.Entities.Design> Design { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(CancellationToken cancellationToken);
        Task RollbackTransactionAsync(CancellationToken cancellationToken);
    }
}
