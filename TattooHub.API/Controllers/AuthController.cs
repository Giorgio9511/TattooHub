using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TattooHub.Application.DTOs.Artist;
using TattooHub.Application.DTOs.Auth;
using TattooHub.Application.Services;
using TattooHub.Infrastructure.Identity;

namespace TattooHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtTokenGenerator _jwtGenerator;
        private readonly ArtistService _artistService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            JwtTokenGenerator jwtGenerator,
            ArtistService artistService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtGenerator = jwtGenerator;
            _artistService = artistService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            //Crear rol si no existe
            if (!await _roleManager.RoleExistsAsync("Artist"))
                await _roleManager.CreateAsync(new IdentityRole("Artist"));

            await _userManager.AddToRoleAsync(user, "Artist");

            //Crear entidad Artist
            await _artistService.CreateArtistAsync(
                user.Id,
                new CreateArtistDto
                {
                    NombreCompleto = request.NombreCompleto,
                    NombreEstudio = request.NombreEstudio,
                    DireccionEstudio = request.DireccionEstudio
                });

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtGenerator.GenerateToken(user, roles);

            return Ok(
                new AuthResponseDto
                {
                    UserId = user.Id,
                    Email = user.Email!,
                    Token = token
                });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return Unauthorized("Credenciales inválidas");

            var validPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!validPassword)
                return Unauthorized("CRedenciales inválidas");

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtGenerator.GenerateToken(user, roles);

            return Ok(new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email!,
                Token = token
            });
        }
    }
}
