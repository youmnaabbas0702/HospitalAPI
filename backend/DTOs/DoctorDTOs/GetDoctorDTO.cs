namespace HospitalSystemAPI.DTOs
{
    public class GetDoctorDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string speciality { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
