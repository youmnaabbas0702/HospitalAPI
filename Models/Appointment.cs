using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSystemAPI.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [ForeignKey("Doctor")]

        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public Patient? Patient { get; set; } = default!;
        public Doctor? Doctor { get; set; } = default!;
    }
}
