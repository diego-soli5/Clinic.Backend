namespace Clinic.Infrastructure.Responses
{
    public class NotFoundResponse
    {
        public NotFoundResponse()
        {
            Message = "No se encontró el recurso solicitado.";
        }

        public NotFoundResponse(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
