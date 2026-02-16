using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TattooHub.Application.DTOs.Auth
{
    public class RegisterRequestDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string NombreCompleto { get; set; } = default!;
        public string NombreEstudio { get; set; } = default!;
        public string DireccionEstudio { get; set; } = default!;
    }
}
