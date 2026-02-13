using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TattooHub.Domain.Common;
using TattooHub.Domain.Enums;

namespace TattooHub.Domain.Entities
{
    //Representa al artista en la plataforma
    public class Artist : BaseEntity
    {
        //Información basica
        public string NombreCompleto { get; private set; }
        public string Email { get; private set; }
        public string? Telefono { get; private set; }
        public string? Bio {  get; private set; }

        //Configuración profesional
        public string NombreEstudio { get; private set; }
        public string DireccionEstudio { get; private set; }
        public string? Instagram { get; private set; }
        public List<TattooStyle> Especialidades { get; private set; }

        //SaaS
        public SubscriptionTier SubscriptionTier { get; private set; }
        public DateTime? SubscriptionExpiresAt { get; private set; }
        public bool EstaActivo {  get; private set; }

        //Navegación (relaciones)
        public List<PortfolioItem> PortfolioItems { get; private set; }
        public List<Design> Designs { get; private set; }

        private Artist()
        {
            //Constructor privado para EF Core
            PortfolioItems = new List<PortfolioItem>();
            Designs = new List<Design>();
            Especialidades = new List<TattooStyle>();
        }

        //Constructor para crear un nuevo artista
        public Artist(
            string nombreCompleto,
            string email,
            string nombreEstudio,
            string direccionEstudio)
            : base ()
        {
            SetNombreCompleto(nombreCompleto);
            SetEmail(email);
            NombreEstudio = nombreEstudio;
            DireccionEstudio = direccionEstudio;

            SubscriptionTier = SubscriptionTier.Free;
            EstaActivo = true;
            Especialidades = new List<TattooStyle>();
            PortfolioItems = new List<PortfolioItem>();
            Designs = new List<Design>();
        }

        //Metodos de negocio (encapsulacion)
        public void SetNombreCompleto(string nombreCompleto)
        {
            if (string.IsNullOrWhiteSpace(nombreCompleto))
                throw new ArgumentException("El nombre completo es requerido", nameof(nombreCompleto));

            if (nombreCompleto.Length < 3)
                throw new ArgumentException("El nombre debe tener al menos 3 caracteres", nameof(nombreCompleto));

            NombreCompleto = nombreCompleto;
            MarkAsUpdated();
        }

        public void SetEmail(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email es requerido", nameof(email));
            if (!email.Contains("@"))
                throw new ArgumentException("Email invalido", nameof(email));

            Email = email.ToLowerInvariant();
            MarkAsUpdated();
        }

        public void UpdateBio(string bio)
        {
            if(bio?.Length > 500)
                throw new ArgumentException("La biografia no puede exceder los 500 caracteres", nameof(bio));

            Bio = bio;
            MarkAsUpdated();
        }

        public void AddEspecialidad(TattooStyle style)
        {
            if(!Especialidades.Contains(style))
            {
                Especialidades.Add(style);
                MarkAsUpdated();
            }
        }

        public void UpgradeSubscripcion(SubscriptionTier newTier, int durationMonths)
        {
            if (newTier < SubscriptionTier)
                throw new InvalidOperationException("No se puede degradar la suscripción de esta forma");

            SubscriptionTier = newTier;
            SubscriptionExpiresAt = DateTime.UtcNow.AddMonths(durationMonths);
            MarkAsUpdated();
        }

        public void Desactivar()
        {
            EstaActivo = false;
            MarkAsUpdated();
        }

        public void Activate()
        {
            EstaActivo = true;
            MarkAsUpdated();
        }
    }
}
