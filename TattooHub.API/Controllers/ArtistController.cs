using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TattooHub.Application.DTOs.Artist;
using TattooHub.Application.Services;
using TattooHub.Domain.Exceptions;

namespace TattooHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly ArtistService _artistService;

        public ArtistController(ArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllActive(CancellationToken cancellationToken)
        {
            var artist = await _artistService.GetAllActiveArtistsAsync(cancellationToken);
            return Ok(artist);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var artist = await _artistService.GetArtistByIdAsync(id, cancellationToken);
                return Ok(artist);
            }
            catch(ArtistNotFoundException)
            {
                return NotFound(new { message = "Artista no encontrado" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateArtistDto dto,
            CancellationToken cancellationToken)
        {
            try
            {
                var artist = await _artistService.CreateArtistAsync("TEMP_USER_ID" , dto, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { id = artist.Id }, artist);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateArtistDto dto,
            CancellationToken cancellationToken)
        {
            try
            {
                var artist = await _artistService.UpdateArtistAsync(id, dto, cancellationToken);
                return Ok(artist);
            }
            catch (ArtistNotFoundException)
            {
                return NotFound(new { message = "Artista no encontrado" });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Desactivar(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _artistService.DesactivarArtistaAsync(id, cancellationToken);
                return NoContent();
            }
            catch (ArtistNotFoundException)
            {
                return NotFound(new { message = "Artista no encontradp" });
            }
        }
    }
}
