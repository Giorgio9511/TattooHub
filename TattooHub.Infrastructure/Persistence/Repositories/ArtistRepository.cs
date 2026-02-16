using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TattooHub.Application.Interfaces.Persistence;
using TattooHub.Domain.Entities;

namespace TattooHub.Infrastructure.Persistence.Repositories
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        public ArtistRepository(ApplicationDbContext context) : base(context)
        { 
        }

        public async Task<IEnumerable<Artist>> GetActiveArtistsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(a => a.EstaActivo)
                .OrderBy(a => a.NombreCompleto)
                .ToListAsync(cancellationToken);
        }
    }
}
