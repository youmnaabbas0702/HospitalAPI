namespace HospitalSystemAPI.Services
{
    public interface IIdGenerator
    {
        string GenerateDoctorId();
        string GeneratePatientId();
    }

}
