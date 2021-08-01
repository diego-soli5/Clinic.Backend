namespace Clinic.Core.DTOs.Medic
{
    public class MedicUpdateDTO
    {
        public int Id { get; set; }
        public int IdConsultingRoom { get; set; }
        public int IdMedicalSpecialty { get; set; }
        public string Names { get; set; }
        public string Surnames { get; set; }
        public int? Identification { get; set; }
    }
}
