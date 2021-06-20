using System;
using System.Collections.Generic;

namespace Clinic.Core.Entities
{
    public class ClinicalHistory : BaseEntity
    {
        public int IdPatient { get; set; }
        public DateTime CreationDate { get; set; }

        public Patient Patient { get; set; }
        public IEnumerable<Diagnostic> Diagnostics { get; set; }
    }
}
