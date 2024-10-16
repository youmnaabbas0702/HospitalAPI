namespace HospitalSystemAPI.DTOs.AppointmentDTOs
{
    public class ViewAppointments
    {
        public string PatientId { get; set; }

        public string DoctorId { get; set; }
        public string? PatientName { get; set; }
        public string? DoctortName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int AppointmentId { get; internal set; }
    }
}
