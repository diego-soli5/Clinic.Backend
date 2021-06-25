namespace Clinic.Core.DTOs.Account
{
    public class LoginRequestDTO
    {
        public dynamic EmailOrIdentification { get; set; }
        public string Password { get; set; }
    }
}
