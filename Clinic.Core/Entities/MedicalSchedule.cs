using Clinic.Core.Enumerations;
using System.Collections.Generic;

namespace Clinic.Core.Entities
{
    public class MedicalSchedule : BaseEntity
    {
        public System.DayOfWeek DayOfWeek { get; set; }
        public int IdMedic { get; set; }
        public EntityStatus EntityStatus { get; set; }

        public Medic Medic { get; set; }
        public IEnumerable<MedicAttentionHour> MedicAttentionHours { get; set; }
    }
}
