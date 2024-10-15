namespace HospitalSystemAPI.DTOs.DoctorDTOs
{
    public class DoctorScheduleDTO
    {
        public string DoctorName { get; set; } = string.Empty;
        public DayOfWeek Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
