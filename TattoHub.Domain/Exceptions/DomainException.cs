using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TattooHub.Domain.Exceptions
{
    //Ecxepción base para errores del dominio
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message) 
        {
        
        }

        protected DomainException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
