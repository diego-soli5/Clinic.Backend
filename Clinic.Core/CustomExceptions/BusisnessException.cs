using System;

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
