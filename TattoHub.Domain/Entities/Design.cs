using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TattooHub.Domain.Common;
using TattooHub.Domain.Enums;

namespace TattooHub.Domain.Entities
{
    //Representa un diseño de tatuaje (flash) disponible para venta
    public class Design : BaseEntity
    {
        public Guid ArtistId { get; private set; }
        public string Titulo { get; private set; }
        public string Descripcion { get; private set; }
        public string ImagenUrl { get; private set; }
        public decimal Precio { get; private set; }
        public TattooStyle Style { get; private set; }
        public DesignStatus Status { get; private set; }
        public bool EsUnico { get; private set; } //Si es verdadero, solo se vende una vez

        //Navegación
        public Artist Artist { get; private set; } = null!;

        private Design() { }

        public Design(
            Guid artistId,
            string descripcion,
            string titulo,
            string imagenUrl,
            decimal precio,
            TattooStyle style,
            bool esUnico = false)
            : base()
        {
            ArtistId = artistId;
            SetTitulo(titulo);
            Descripcion = descripcion;
            SetImagenUrl(imagenUrl);
            SetPrecio(precio);
            Style = style;
            EsUnico = esUnico;
            Status = DesignStatus.Draft;
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
                throw new ArgumentException("La URL de imagen es requerida", nameof(imagenUrl));

            ImagenUrl = imagenUrl;
            MarkAsUpdated();
        }

        public void SetPrecio(decimal precio)
        {
            if(precio < 0)
                throw new ArgumentException("El precio no puede ser negativo", nameof(precio));

            Precio = precio;
            MarkAsUpdated();
        }

        public void Pubicar()
        {
            if (Status != DesignStatus.Draft)
                throw new InvalidOperationException("Solo se pueden publicar borradores");

            Status = DesignStatus.Active;
            MarkAsUpdated();
        }

        public void MarcarComoVendido()
        {
            if (!EsUnico)
                throw new InvalidOperationException("Solo diseños únicos pueden marcarse como vendidos");

            if (Status != DesignStatus.Active)
                throw new InvalidOperationException("Solo diseños activos pueden venderse");

            Status = DesignStatus.Sold;
            MarkAsUpdated();
        }

        public void Archive()
        {
            Status = DesignStatus.Archived;
            MarkAsUpdated();
        }
    }
}
