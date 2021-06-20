using Clinic.Core.Enumerations;
using System;

namespace Clinic.Core.Entities
{
    public class Diagnostic : BaseEntity
    {
        public string Description { get; set; }
        public string Observations { get; set; }
        public int IdClinicalHistory { get; set; }
        public DateTime CreatedDate { get; set; }
        public EntityStatus EntityStatus { get; set; }

        public ClinicalHistory ClinicalHistory { get; set; }
    }
}
