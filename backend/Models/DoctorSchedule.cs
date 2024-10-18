namespace HospitalSystemAPI.Models
{
    public class DoctorSchedule
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public Doctor Doctor { get; set; } = default!;
    }
}
