using HospitalSystemAPI.Data;
using HospitalSystemAPI.DTOs;
using HospitalSystemAPI.DTOs.PatientDTOs;
using HospitalSystemAPI.Models;
using HospitalSystemAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdGenerator _idGenerator;
        private readonly HospitalDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, IIdGenerator idGenerator, HospitalDbContext context)
        {
            _userManager = userManager;
            _idGenerator = idGenerator;
            _context = context;
        }

        // Doctor Registration
        [HttpPost("register-doctor")]
        public async Task<IActionResult> RegisterDoctor([FromBody] DoctorInsertionDTO dto)
        {
            // Generate unique DoctorId
            var doctorId = _idGenerator.GenerateDoctorId();

            var doctor = new Doctor
            {
                Email = dto.Email,
                Name = dto.Name,
                SpecialityId = dto.SpecialityId,
                Id = doctorId
            };

            var result = await _userManager.CreateAsync(doctor, dto.Password);
            if (result.Succeeded)
            {
                // Assign "Doctor" role
                await _userManager.AddToRoleAsync(doctor, "Doctor");
                return Ok(new { DoctorId = doctor.Id, Message = "Doctor registered successfully." });
            }

            // Return errors
            return BadRequest(result.Errors);
        }

        // Patient Registration
        [HttpPost("register-patient")]
        public async Task<IActionResult> RegisterPatient([FromBody] PatientInsertionDTO dto)
        {
            // Generate unique PatientId
            var patientId = _idGenerator.GeneratePatientId();

            var patient = new Patient
            {
                Email = dto.Email,
                Name = dto.Name,
                BirthDate = dto.BirthDate,
                Id = patientId
            };

            var result = await _userManager.CreateAsync(patient, dto.Password);
            if (result.Succeeded)
            {
                // Assign "Patient" role
                await _userManager.AddToRoleAsync(patient, "Patient");
                return Ok(new { PatientId = patient.Id, Message = "Patient registered successfully." });
            }

            // Return errors
            return BadRequest(result.Errors);
        }
    }
}


