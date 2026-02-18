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
        public IList<string> Roles { get; set; } = default!;

        public AuthResponseDto(string userId, string email, string token, IList<string> roles)
        {
            UserId = userId;
            Email = email;
            Token = token;
            Roles = roles;
        }
    }
}
