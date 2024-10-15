namespace HospitalSystemAPI.Models
{
    public class MedicalHistory
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Condition { get; set; } = string.Empty;
        public DateTime RecordDate { get; set; }
        public string Treatment { get; set; } = string.Empty;
    }
}
