using System.Collections.Generic;

namespace Clinic.Infrastructure.Responses
{
    public class BadRequestResponse
    {
        public string Message { get; set; }
        public List<string> ModelErrors { get; set; }
        public bool HasModelErrors => ModelErrors != null;
    }
}
