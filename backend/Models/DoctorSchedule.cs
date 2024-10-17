namespace HospitalSystemAPI.Models
{
    public class DoctorSchedule
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public DayOfWeek Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Doctor Doctor { get; set; } = default!;
    }
}
