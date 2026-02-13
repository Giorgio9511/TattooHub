using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TattoHub.Domain.Exceptions
{
    public class ArtistNotFoundException : DomainException
    {
        public ArtistNotFoundException(Guid artistaId) 
            : base($"No se encontró el artista con ID: {artistaId}")
        {
        }
    }
}
