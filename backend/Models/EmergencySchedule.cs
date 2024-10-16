using System.ComponentModel.DataAnnotations;

namespace HospitalSystemAPI.Models
{
    public class EmergencySchedule
    {
        [Key]
        public int ShiftId { get; set; }
        public DayOfWeek Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; } = default!;
    }
}
