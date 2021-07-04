using Clinic.Core.Enumerations;
using System;

namespace Clinic.Core.DTOs.Employee
{
    public class EmployeeListDTO
    {
        public int Id { get; set; }
        public int Identification { get; set; }
        public string FullName { get; set; }
        public DateTime HireDate { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; }
    }
}
