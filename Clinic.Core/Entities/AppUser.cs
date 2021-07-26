using Clinic.Core.Enumerations;

namespace Clinic.Core.Entities
{
    public class AppUser : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public AppUserRole Role { get; set; }
        public EntityStatus EntityStatus { get; set; }
        public string SMToken { get; set; }

        public Employee Employee { get; set; }
    }
}
