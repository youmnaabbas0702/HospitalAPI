namespace HospitalSystemAPI
{
    public class MedicalRecordDto

    {
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public bool AddToMedicalHistory { get; set; }
    }
}
