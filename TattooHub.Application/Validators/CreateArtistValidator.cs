using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TattooHub.Application.DTOs.Artist;

namespace TattooHub.Application.Validators
{
    public class CreateArtistValidator : AbstractValidator<CreateArtistDto>
    {
        public CreateArtistValidator()
        {
            RuleFor(x => x.NombreCompleto)
                .NotEmpty().WithMessage("El nombre es requerido")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres");

            RuleFor(x => x.NombreEstudio)
                .NotEmpty().WithMessage("El nombre del estudio es requerido")
                .MaximumLength(200);

            RuleFor(x => x.DireccionEstudio)
                .NotEmpty().WithMessage("La dirección del estudio es requerida")
                .MaximumLength(300);

            RuleFor(x => x.Telefono)
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .When(x => !string.IsNullOrEmpty(x.Telefono))
                .WithMessage("Formato de télefono inválido");
        }
    }
}
