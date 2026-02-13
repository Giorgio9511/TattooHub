using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TattooHub.Domain.Enums;

namespace TattooHub.Application.DTOs.Artist
{
    public class UpdateArtistDto
    {
        public string? NombreCompleto { get; set; }
        public string? Telefono { get; set; }
        public string? Bio { get; set; }
        public string? Instagram { get; set; }
        public List<TattooStyle>? Especialidades { get; set; }
    }
}
