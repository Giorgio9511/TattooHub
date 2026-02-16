using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TattooHub.Application.DTOs.Artist
{
    //DTO para crear un nuevo artista
    public class CreateArtistDto
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string NombreEstudio { get; set; } = string.Empty;
        public string DireccionEstudio { get; set; } = string.Empty;
    }
}
