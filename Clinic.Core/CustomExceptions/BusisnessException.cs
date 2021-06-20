using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.CustomExceptions
{
    public class BusisnessException : Exception
    {
        public BusisnessException(string message)
            : base(message)
        { }

        public BusisnessException()
        { }
    }
}
