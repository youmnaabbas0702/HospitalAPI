using HospitalSystemAPI.Data;
using HospitalSystemAPI.DTOs;
using HospitalSystemAPI.DTOs.AccountDTOs;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdGenerator _idGenerator;
        private readonly HospitalDbContext _context;

        public AccountController(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager, IIdGenerator idGenerator, HospitalDbContext context)
        {
            _userManager = userManager;
            _idGenerator = idGenerator;
            _context = context;
            _signInManager = signInManager;
        }

        // Doctor Registration
        [HttpPost("register-doctor")]
        public async Task<IActionResult> RegisterDoctor([FromBody] DoctorInsertionDTO dto)
        {
            // Check if the username already exists
            var existingUserByName = await _userManager.FindByNameAsync(dto.UserName);
            if (existingUserByName != null)
            {
                return BadRequest(new { Message = "Username already exists." });
            }

            // Check if the email already exists
            var existingUserByEmail = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUserByEmail != null)
            {
                return BadRequest(new { Message = "Email already exists." });
            }

            // Generate unique DoctorId
            var doctorId = _idGenerator.GenerateDoctorId();

            var doctor = new Doctor
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Name = dto.Name,
                SpecialityId = dto.SpecialityId,
                Id = doctorId
            };

            // Create doctor with password
            var result = await _userManager.CreateAsync(doctor, dto.Password);
            if (result.Succeeded)
            {
                // Assign "Doctor" role
                await _userManager.AddToRoleAsync(doctor, "Doctor");

                // Return success response
                return Ok(new { DoctorId = doctor.Id, Message = "Doctor registered successfully." });
            }

            // If there were errors during creation, return them
            return BadRequest(result.Errors);
        }

        // Patient Registration
        [HttpPost("register-patient")]
        public async Task<IActionResult> RegisterPatient([FromBody] PatientInsertionDTO dto)
        {
            // Check if the username already exists
            var existingUserByName = await _userManager.FindByNameAsync(dto.UserName);
            if (existingUserByName != null)
            {
                return BadRequest(new { Message = "Username already exists." });
            }

            // Check if the email already exists
            var existingUserByEmail = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUserByEmail != null)
            {
                return BadRequest(new { Message = "Email already exists." });
            }

            // Generate unique PatientId
            var patientId = _idGenerator.GeneratePatientId();

            var patient = new Patient
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Name = dto.Name,
                BirthDate = dto.BirthDate,
                Id = patientId
            };

            // Create patient with password
            var result = await _userManager.CreateAsync(patient, dto.Password);
            if (result.Succeeded)
            {
                // Assign "Patient" role
                await _userManager.AddToRoleAsync(patient, "Patient");

                // Return success response
                return Ok(new { PatientId = patient.Id, Message = "Patient registered successfully." });
            }

            // If there were errors during creation, return them
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.UserName) ?? await _userManager.FindByEmailAsync(loginDTO.UserName);

            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid credentials." });
            }

            // Check the password and sign in
            var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var userResponse = new
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Message = "Login successful!"
                };
                return Ok(userResponse);
            }

            return Unauthorized(new { Message = "Invalid login attempt." });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { Message = "Logout successful." });
        }
    }
}


