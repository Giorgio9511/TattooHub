using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TattooHub.Application.Interfaces.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(
            string userId,
            string email,
            IList<string> roles);
    }
}
