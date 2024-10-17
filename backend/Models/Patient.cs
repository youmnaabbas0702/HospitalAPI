namespace HospitalSystemAPI.Models
{
    public class Patient: ApplicationUser
    {
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } 

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
        public ICollection<MedicalHistory> MedicalHistory { get; set; } = new List<MedicalHistory>();


    }
}
