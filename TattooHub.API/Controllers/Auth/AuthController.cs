using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TattooHub.Application.DTOs.Artist;
using TattooHub.Application.DTOs.Auth;
using TattooHub.Application.Services;
using TattooHub.Infrastructure.Identity;

namespace TattooHub.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _authService.RegistrarArtistAsync(request);

                return Ok(response);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
    }
}
