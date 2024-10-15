namespace HospitalSystemAPI.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; } 
        public string PhoneNumber { get; set; } = string.Empty;
        public int SpecialityId { get; set; }


        public ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();
        public ICollection<MedicalRecord>? MedicalRecords { get; set; } = new List<MedicalRecord>();
        public ICollection<MedicalHistory>? MedicalHistory { get; set; } = new List<MedicalHistory>();


    }
}
