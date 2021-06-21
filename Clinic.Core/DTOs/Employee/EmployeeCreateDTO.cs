using Clinic.Core.DTOs.AppUser;
using Clinic.Core.DTOs.Person;

namespace Clinic.Core.DTOs.Employee
{
    public class EmployeeCreateDTO
    {
        public AppUserDTO AppUser { get; set; }
        public EmployeeDTO Employee { get; set; }
        public PersonDTO Person { get; set; }
    }
}
