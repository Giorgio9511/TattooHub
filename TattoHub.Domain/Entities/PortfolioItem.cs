using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TattoHub.Domain.Common;

namespace TattoHub.Domain.Entities
{
    //Representa un trabajo del portafolio de una artista
    public class PortfolioItem : BaseEntity
    {
        public Guid ArtistId { get; private set; }
        public string Titulo { get; private set; }
        public string? Descripcion { get; private set; }
        public string ImagenUrl { get; private set; } //URL en cloud storage
        public int Orden {  get; private set; }
        public bool IsFeatured { get; private set; }

        //Navegación
        public Artist Artist { get; private set; } = null!;

        private PortfolioItem() { }

        public PortfolioItem(
            Guid artistId,
            string titulo,
            string imagenUrl,
            int orden = 0)
            :base()
        {
            ArtistId = artistId;
            SetTitulo(titulo);
            SetImagenUrl(imagenUrl);
            Orden = orden;
            IsFeatured = false;
        }

        public void SetTitulo(string titulo)
        {
            if(string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("El título es requerido", nameof(titulo));

            Titulo = titulo;
            MarkAsUpdated();
        }

        public void SetImagenUrl(string imagenUrl)
        {
            if(string.IsNullOrWhiteSpace(imagenUrl))
                throw new ArgumentException("La URL de imagen es requerida", nameof (imagenUrl));

            ImagenUrl = imagenUrl;
            MarkAsUpdated();
        }

        public void ToggleFeatured()
        {
            IsFeatured = !IsFeatured;
            MarkAsUpdated();
        }

        public void ReorderTo(int newOrder)
        {
            if(newOrder < 0) 
                throw new ArgumentException("El orden no puede ser negativo", nameof(newOrder));

            Orden = newOrder;
            MarkAsUpdated();
        }
    }
}
