namespace HospitalSystemAPI.DTOs.DoctorDTOs
{
    public class AddScheduleDTO
    {
        public string DoctorId { get; set; }
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
