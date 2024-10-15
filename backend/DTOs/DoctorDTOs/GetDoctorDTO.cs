namespace HospitalSystemAPI.DTOs
{
    public class GetDoctorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string speciality { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
