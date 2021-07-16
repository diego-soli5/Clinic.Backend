using System.Collections.Generic;

namespace Clinic.Core.Entities
{
    public class Medic : BaseEntity
    {
        public int IdConsultingRoom { get; set; }
        public int IdEmployee { get; set; }
        public int IdMedicalSpecialty { get; set; }
        public bool MustUpdateInfo { get; set; }

        public ConsultingRoom ConsultingRoom { get; set; }
        public MedicalSpecialty MedicalSpecialty { get; set; }
        public Employee Employee { get; set; }
        public IEnumerable<MedicalSchedule> MedicalSchedules { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
