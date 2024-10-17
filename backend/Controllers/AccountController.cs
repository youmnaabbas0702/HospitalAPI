using HospitalSystemAPI.Data;
using HospitalSystemAPI.DTOs;
using HospitalSystemAPI.DTOs.AccountDTOs;
using HospitalSystemAPI.DTOs.PatientDTOs;
using HospitalSystemAPI.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HospitalSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticateService _authService;

        public AccountController(IAuthenticateService authService)
        {
            _authService = authService;
        }

        [Authorize(Roles = "genAdmin")]
        [HttpPost("register-doctor")]
        public async Task<IActionResult> RegisterDoctor([FromBody] DoctorInsertionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterDoctorAsync(dto);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [Authorize(Roles = "genAdmin")]
        [HttpPost("register-patient")]
        public async Task<IActionResult> RegisterPatient([FromBody] PatientInsertionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterPatientAsync(dto);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            return Ok(new {Token = result.Token, ExpiresOn = result.ExpiresOn});
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetToken([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.GetTokenAsync(dto);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            return Ok(new { Token = result.Token, ExpiresOn = result.ExpiresOn });
        }
    }

}


