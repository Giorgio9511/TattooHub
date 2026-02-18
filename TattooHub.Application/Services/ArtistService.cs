using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TattooHub.Application.DTOs.Artist;
using TattooHub.Application.DTOs.Auth;
using TattooHub.Application.Interfaces.Persistence;
using TattooHub.Application.Interfaces.Services;
using TattooHub.Domain.Entities;
using TattooHub.Domain.Exceptions;

namespace TattooHub.Application.Services
{
    //Servicio de aplicación para gestionar artistas
    //Implementa los casos de uso
    public class ArtistService
    {
        private readonly IUnitOfWork _unidadTrabajo;
        private readonly IIdentityService _identityService;

        public ArtistService(IUnitOfWork unidadTrabajo, IIdentityService identityService)
        {
            _unidadTrabajo = unidadTrabajo;
            _identityService = identityService;
        }

        public async Task<ArtistDto> CreateArtistAsync(
            CreateArtistDto dto,
            CancellationToken cancellationToken = default)
        {
            if (await _identityService.UserExistsAsync(dto.Email))
                throw new InvalidOperationException("Usuario ya existe");

            var userId = await _identityService.CreateUserAsync(dto.Email, dto.Password);

            await _identityService.AddUserToRoleAsync(userId, "Artist");

            //Crear la entidad del dominio
            var artist = new Artist(
                userId,
                dto.NombreCompleto,
                dto.NombreEstudio,
                dto.DireccionEstudio);

            if(!string.IsNullOrWhiteSpace(dto.Telefono))
            {
                //Aqui iria la logica de validacion/seteo del telefono 
            }

            //Persistir
            await _unidadTrabajo.Artist.AddAsync(artist, cancellationToken);
            await _unidadTrabajo.SaveChangesAsync(cancellationToken);

            //Mapear a DTO y devolver
            return MapToDto(artist);
        }

        public async Task<ArtistDto> GetArtistByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var artist = await _unidadTrabajo.Artist.GetByIdAsync(id, cancellationToken);

            if(artist == null) 
                throw new ArtistNotFoundException(id);

            return MapToDto(artist);
        }

        public async Task<IEnumerable<ArtistDto>> GetAllActiveArtistsAsync(
            CancellationToken cancellationToken)
        {
            var artist = await _unidadTrabajo.Artist.GetActiveArtistsAsync(cancellationToken);
            return artist.Select(MapToDto);
        }

        public async Task<ArtistDto> UpdateArtistAsync(
            Guid id,
            UpdateArtistDto dto,
            CancellationToken cancellationToken = default)
        {
            var artist = await _unidadTrabajo.Artist.GetByIdAsync(id, cancellationToken);

            if(artist == null)
                throw new ArtistNotFoundException(id);

            //Aplicar los cambios usando métodos del dominio
            if(!string.IsNullOrWhiteSpace(dto.NombreCompleto))
                artist.SetNombreCompleto(dto.NombreCompleto);

            if (!string.IsNullOrWhiteSpace(dto.Bio))
                artist.UpdateBio(dto.Bio);

            if(dto.Especialidades != null)
            {
                foreach (var style in dto.Especialidades)
                {
                    artist.AddEspecialidad(style);
                }
            }

            await _unidadTrabajo.SaveChangesAsync(cancellationToken);
            return MapToDto(artist);
        }

        public async Task DesactivarArtistaAsync(
            Guid id,
            CancellationToken cancellationToken= default)
        {
            var artist = await _unidadTrabajo.Artist.GetByIdAsync(id, cancellationToken);

            if(artist == null)
                throw new ArtistNotFoundException(id);

            artist.Desactivar();
            await _unidadTrabajo.SaveChangesAsync(cancellationToken);
        }

        //Helper para mapear Entity -> DTO
        private static ArtistDto MapToDto(Artist artist)
        {
            return new ArtistDto
            {
                Id = artist.Id,
                UserId = artist.UserId,
                NombreCompleto = artist.NombreCompleto,
                Telefono = artist.Telefono,
                Bio = artist.Bio,
                NombreEstudio = artist.NombreEstudio,
                DireccionEstudio = artist.DireccionEstudio,
                Instagram = artist.Instagram,
                Especialidades = artist.Especialidades,
                SubscriptionTier = artist.SubscriptionTier,
                EstaActivo = artist.EstaActivo,
                FechaCreacion = artist.CreatedAt
            };
        }
    }
}
