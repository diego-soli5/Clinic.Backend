namespace Clinic.Core.DTOs.Account
{
    public class ChangePasswordDTO
    {
        public int Id { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }
}
