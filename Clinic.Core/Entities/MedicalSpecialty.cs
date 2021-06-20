using Clinic.Core.Enumerations;
using System.Collections.Generic;

namespace Clinic.Core.Entities
{
    public class MedicalSpecialty : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public EntityStatus EntityStatus { get; set; }

        public IEnumerable<Medic> Medics { get; set; }
    }
}
