using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TattooHub.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public Guid? ArtistId { get; set; }
    }
}
