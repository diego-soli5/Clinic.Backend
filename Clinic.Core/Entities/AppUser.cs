using Clinic.Core.Enumerations;
using System.Collections.Generic;

namespace Clinic.Core.Entities
{
    public class AppUser : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public AppUserRole Role { get; set; }
        public EntityStatus EntityStatus { get; set; }

        public Employee Employee { get; set; }
    }
}
