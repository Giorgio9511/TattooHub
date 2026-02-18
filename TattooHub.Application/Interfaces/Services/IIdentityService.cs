using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TattooHub.Application.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<string> CreateUserAsync(string email, string password);
        Task AddUserToRoleAsync(string userId, string role);
        Task<bool> UserExistsAsync(string email);
        Task<string> GetUserByEmailAsync(string email);
        Task<IList<string>> GetRolesAsync(string userId);
        Task<bool> CheckPasswordAsync(string email, string password);
    }
}
