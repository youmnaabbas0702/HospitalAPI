namespace HospitalSystemAPI.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int SpecialityId { get; set; }

        public string Email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public Speciality Speciality { get; set; } = default;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    }
}
