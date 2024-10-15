namespace HospitalSystemAPI.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public Patient Patient { get; set; } = default!;
        public Doctor Doctor { get; set; } = default!;
    }
}
