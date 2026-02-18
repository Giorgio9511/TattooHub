using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TattooHub.Application.DTOs.Auth;
using TattooHub.Application.Interfaces.Persistence;
using TattooHub.Application.Interfaces.Services;
using TattooHub.Domain.Entities;

namespace TattooHub.Application.Services
{
    //Servicio para gestionar autenticacion y registro de usuario
    public class AuthService
    {
        private readonly IUnitOfWork _unidadTrabajo;
        private readonly IIdentityService _identityService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUnitOfWork unidadTrabajo, IIdentityService identityService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unidadTrabajo = unidadTrabajo;
            _identityService = identityService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthResponseDto> RegistrarArtistAsync(
            RegisterRequestDto request, 
            CancellationToken cancellationToken = default)
        {
            if (await _identityService.UserExistsAsync(request.Email))
                throw new InvalidOperationException("Ya existe un usuario con ese nombre");

            var userId = await _identityService.CreateUserAsync(request.Email, request.Password);

            await _identityService.AddUserToRoleAsync(userId, "Artist");

            var roles = await _identityService.GetRolesAsync(userId);

            var token = _jwtTokenGenerator.GenerateToken(userId, request.Email, roles);

            //Crear la entidad del dominio
            var artist = new Artist(
                userId,
                request.NombreCompleto,
                request.NombreEstudio,
                request.DireccionEstudio);

            //Persistir
            await _unidadTrabajo.Artist.AddAsync(artist, cancellationToken);
            await _unidadTrabajo.SaveChangesAsync(cancellationToken);

            var respuesta = new AuthResponseDto(userId, request.Email, token, roles);

            return respuesta;
        }

        public async Task<AuthResponseDto> LoginAsync(
            LoginRequestDto request,
            CancellationToken cancellationToken = default)
        {
            if (!await _identityService.UserExistsAsync(request.Email)
                || !await _identityService.CheckPasswordAsync(request.Email, request.Password))
                throw new UnauthorizedAccessException("Credenciales inválidas");

            var userId = await _identityService.GetUserByEmailAsync(request.Email);

            var roles = await _identityService.GetRolesAsync(userId);

            var token = _jwtTokenGenerator.GenerateToken(userId, request.Email, roles);

            var respuesta = new AuthResponseDto(
                userId,
                request.Email,
                token, 
                roles);

            return respuesta;
        }
    }
}
