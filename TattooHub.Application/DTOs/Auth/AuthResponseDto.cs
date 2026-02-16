using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TattooHub.Application.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string UserId { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
