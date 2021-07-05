using Clinic.Core.DTOs.Person;
using Clinic.Core.Enumerations;
using Microsoft.AspNetCore.Http;

namespace Clinic.Core.DTOs.Employee
{
    public class EmployeeUpdateDTO
    {
        public int Id { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        public PersonUpdateDTO Person { get; set; }
        public IFormFile Image { get; set; }
    }
}
