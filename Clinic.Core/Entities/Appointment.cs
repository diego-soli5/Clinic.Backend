using Clinic.Core.Enumerations;
using System;

namespace Clinic.Core.Entities
{
    public class Appointment : BaseEntity
    {
        public int IdMedicAttentionHour { get; set; }
        public int IdMedic { get; set; }
        public int IdPacient { get; set; }
        public DateTime Date { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }

        public MedicAttentionHour MedicAttentionHour { get; set; }
        public Medic Medic { get; set; }
        public Patient Patient { get; set; }
    }
}
