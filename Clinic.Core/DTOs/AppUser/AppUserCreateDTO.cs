using Clinic.Core.Enumerations;

namespace Clinic.Core.DTOs.AppUser
{
    public class AppUserCreateDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public AppUserRole Role { get; set; }
    }
}
