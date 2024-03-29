﻿namespace Clinic.Infrastructure.Responses
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

        public NotFoundResponse(string message, int id)
        {
            Message = message;
            NotFoundResourceId = id;
        }

        public string Message { get; set; }
        public int? NotFoundResourceId { get; set; }
    }
}
