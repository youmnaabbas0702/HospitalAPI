using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSystemAPI.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public string PatientId { get; set; }

        public string DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public Patient Patient { get; set; } = default!;
        public Doctor Doctor { get; set; } = default!;
    }
}
