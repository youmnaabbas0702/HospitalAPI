using HospitalSystemAPI.DTOs.PatientDTOs;
using HospitalSystemAPI.DTOs;
using HospitalSystemAPI.Models;
using HospitalSystemAPI.DTOs.AccountDTOs;

namespace HospitalSystemAPI.Services.Authentication
{
    public interface IAuthenticateService
    {
        Task<AuthenticationObject> RegisterDoctorAsync(DoctorInsertionDTO dto);
        Task<AuthenticationObject> RegisterPatientAsync(PatientInsertionDTO dto);
        Task<AuthenticationObject> GetTokenAsync(LoginDTO loginDto);
    }

}
