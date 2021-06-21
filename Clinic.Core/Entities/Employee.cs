using Clinic.Core.Enumerations;
using System;

namespace Clinic.Core.Entities
{
    public class Employee : BaseEntity
    {
        public int? IdAppUser { get; set; }
        public int IdPerson { get; set; }
        public DateTime HireDate { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; }

        public AppUser AppUser { get; set; }
        public Person Person { get; set; }
        public Medic Medic { get; set; }
    }
}
