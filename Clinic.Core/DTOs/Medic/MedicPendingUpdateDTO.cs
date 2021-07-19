namespace Clinic.Core.DTOs.Medic
{
    public class MedicPendingUpdateDTO
    {
        public int IdEmployee { get; set; }
        public int IdConsultingRoom { get; set; }
        public int IdMedicalSpecialty { get; set; }

        //Just to display
        public string Names { get; set; }
        public string Surnames { get; set; }
        public int Identification { get; set; }
    }
}
