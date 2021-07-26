using System;

namespace Clinic.Core.CustomExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {

        }

        public NotFoundException(string message)
            : base(message)
        {

        }

        public NotFoundException(string message, int id)
            : base(message)
        {
            NotFoundResourceId = id;
        }

        public int? NotFoundResourceId { get; set; }
    }
}
