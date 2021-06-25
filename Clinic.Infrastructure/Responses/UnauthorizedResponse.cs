namespace Clinic.Infrastructure.Responses
{
    public class UnauthorizedResponse
    {
        public UnauthorizedResponse()
        {
            Message = "No estás autorizado";
        }

        public UnauthorizedResponse(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
