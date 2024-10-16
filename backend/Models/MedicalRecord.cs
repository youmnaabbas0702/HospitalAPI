namespace HospitalSystemAPI.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string Prescription { get; set; } = string.Empty;
        public bool AddToMedicalHistory { get; set; }
        public Patient Patient { get; set; } = default!;
        public Doctor Doctor { get; set; } = default!;

    }
}
