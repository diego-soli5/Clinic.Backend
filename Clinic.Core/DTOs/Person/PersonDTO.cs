using System;

namespace Clinic.Core.DTOs.Person
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public int Identification { get; set; }
        public string Names { get; set; }
        public string Surnames { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public string ImageName { get; set; }
    }
}
