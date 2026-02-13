using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TattooHub.Domain.Entities;

namespace TattooHub.Application.Interfaces.Persistence
{
    //Repositorio específico para Artist con métodos adicionales
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<Artist?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<IEnumerable<Artist>> GetActiveArtistsAsync(CancellationToken cancellationToken = default);
        Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    }
}
