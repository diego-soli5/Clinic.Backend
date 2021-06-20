using Clinic.Core.Enumerations;
using System.Collections.Generic;

namespace Clinic.Core.Entities
{
    public class Patient : BaseEntity
    { 
        public int IdPerson { get; set; }
        public EntityStatus EntityStatus { get; set; }
        public PatientStatus PatientStatus { get; set; }

        public Person Person { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
        public ClinicalHistory ClinicalHistory { get; set; }
    }
}
