namespace HospitalSystemAPI.Models
{
    public class Doctor: ApplicationUser
    {
        public string Name { get; set; } = string.Empty;
        public int SpecialityId { get; set; }

        public Speciality Speciality { get; set; } = default!;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    }
}
