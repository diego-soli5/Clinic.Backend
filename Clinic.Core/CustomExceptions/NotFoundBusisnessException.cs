using System;

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
