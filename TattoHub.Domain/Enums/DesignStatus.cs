using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TattooHub.Domain.Enums
{
    public enum DesignStatus
    {
        Draft = 0, //Borrador, no visible
        Active = 1, //Publicado disponible
        Sold = 2, //Vendido (Si es diseño unico)
        Archived = 3 //Archivado por el artista
    }
}
