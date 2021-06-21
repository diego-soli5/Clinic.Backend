using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.CustomExceptions
{
    public class NotFoundBusisnessException : Exception
    {
        public NotFoundBusisnessException()
        {

        }

        public NotFoundBusisnessException(string message)
            : base(message)
        {

        }
    }
}
