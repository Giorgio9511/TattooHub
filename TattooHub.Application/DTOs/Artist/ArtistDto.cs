using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TattooHub.Domain.Enums;

namespace TattooHub.Application.DTOs.Artist
{
    //DTO para devolver información de un artista
    public class ArtistDto
    {
        public Guid Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } =string.Empty;
        public string? Telefono { get; set; }
        public string? Bio { get; set; }
        public string NombreEstudio { get; set; } = string.Empty;
        public string DireccionEstudio {  get; set; } = string.Empty;
        public string? Instagram { get; set; }
        public List<TattooStyle> Especialidades { get; set; } = new();
        public SubscriptionTier SubscriptionTier { get; set; }
        public bool EstaActivo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
