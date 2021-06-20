using Clinic.Core.Enumerations;
using System.Collections.Generic;

namespace Clinic.Core.Entities
{
    public class MedicAttentionHour : BaseEntity
    {
        public string Hour { get; set; }
        public int IdMedicalSchedule { get; set; }
        public EntityStatus EntityStatus { get; set; }

        public MedicalSchedule MedicalSchedule { get; set; }
    }
}
