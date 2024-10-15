using HospitalSystemAPI.Models;

namespace HospitalSystemAPI.DTOs.PatientDTOs
{
    public class GetPatientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;
        public IEnumerable<MedicalHistory> MedicalHistory { get; set; } = new List<MedicalHistory>();
    }
}
