using System;

namespace Clinic.Core.Entities
{
    public class Person : BaseEntity
    {
        public int Identification { get; set; }
        public string Names { get; set; }
        public string Surnames { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public string ImageName { get; set; }

        public Employee Employee { get; set; }
        public Patient Patient { get; set; }
    }
}
