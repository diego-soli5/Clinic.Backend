namespace Clinic.Infrastructure.Response
{
    public class BadRequestResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
