namespace Clinic.Core.QueryFilters
{
    public class MedicQueryFilter : BaseQueryFilter
    {
        public int? Identification { get; set; }
        public int? MedicalSpecialty { get; set; }
    }
}
