﻿namespace Clinic.Core.DTOs.Account
{
    public class LoginRequestDTO
    {
        public string EmailOrIdentification { get; set; }
        public string Password { get; set; }
    }
}
