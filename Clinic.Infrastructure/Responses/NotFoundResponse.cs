using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Responses
{
    public class NotFoundResponse
    {
        public NotFoundResponse()
        {
            Message = "No se encontró el recurso solicitado.";
        }

        public string Message { get; set; }
    }
}
