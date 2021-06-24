using Clinic.Core.DTOs.Person;
using Microsoft.AspNetCore.Http;

namespace Clinic.Core.DTOs.Employee
{
    public class EmployeeUpdateDTO
    {
        public PersonUpdateDTO Person { get; set; }
        public IFormFile Image { get; set; }
    }
}
