using Clinic.Core.DTOs.AppUser;
using Clinic.Core.DTOs.Person;
using Clinic.Core.Enumerations;
using System;

namespace Clinic.Core.DTOs.Employee
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; }
        public DateTime HireDate { get; set; }
        public AppUserDTO AppUser { get; set; }
        public PersonDTO Person { get; set; }
    }
}
