using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TattooHub.Domain.Entities;
using TattooHub.Domain.Enums;

namespace TattooHub.Infrastructure.Persistence.Configurations
{
    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.ToTable("Artist");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.NombreCompleto)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(a => a.Email)
                .IsUnique();

            builder.Property(a => a.Bio)
                .HasMaxLength(500);

            builder.Property(a => a.NombreEstudio)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.DireccionEstudio)
                .IsRequired()
                .HasMaxLength(300);

            // Conversión de List<TattooStyle> a string separado por comas
            builder.Property(a => a.Especialidades)
                .HasConversion(
                    v => string.Join(',', v.Select(s => (int)s)),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                          .Select(s => (TattooStyle)int.Parse(s))
                          .ToList()
                );

            // Relaciones
            builder.HasMany(a => a.PortfolioItems)
                .WithOne(p => p.Artist)
                .HasForeignKey(p => p.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Designs)
                .WithOne(d => d.Artist)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
