using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalSystemAPI.Data;
using HospitalSystemAPI.Models;
using HospitalSystemAPI.DTOs.PatientDTOs;
using HospitalSystemAPI.DTOs;
using HospitalSystemAPI.Services;
using System.Numerics;
using Microsoft.AspNetCore.Authorization;

namespace HospitalSystemAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly HospitalDbContext _context;
        private readonly IIdGenerator _generator;

        public PatientController(HospitalDbContext context, IIdGenerator idGenerator)
        {
            _context = context;
            _generator = idGenerator;
        }

        [Authorize(Roles = "genAdmin")]
        // GET: api/Patient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPatientDTO>>> GetPatients()
        {
            var patients = await _context.Patients.ToListAsync();

            List<GetPatientDTO> PatientsObject = getPatientsDTOobject(patients);

            return PatientsObject;
        }

        // GET: api/Patient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPatientDTO>> GetPatient(string id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            GetPatientDTO PatientObject = new GetPatientDTO()
            {
                Id = patient.Id,
                Name = patient.Name,
                UserName = patient.UserName,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                BirthDate = patient.BirthDate
            };
            return PatientObject;
        }

        [Authorize(Roles = "genAdmin")]
        // PUT: api/Patient/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(string id, EditPatientDTO patientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPatient = await _context.Patients.FindAsync(id);
            if (existingPatient == null)
            {
                return NotFound();
            }

            existingPatient.UserName = patientDto.UserName;
            existingPatient.Email = patientDto.Email;
            existingPatient.PasswordHash = patientDto.Password;
            existingPatient.PhoneNumber = patientDto.PhoneNumber;

            return Ok("patient edited");
        }

        [Authorize(Roles = "genAdmin")]
        // DELETE: api/Patient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(string id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(string id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }

        private List<GetPatientDTO> getPatientsDTOobject(List<Patient> patients)
        {
            List<GetPatientDTO> PatientsObject = new List<GetPatientDTO>();
            foreach (var patient in patients)
            {
                PatientsObject.Add(new GetPatientDTO()
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    UserName = patient.UserName,
                    Email = patient.Email,
                    PhoneNumber = patient.PhoneNumber,
                    BirthDate = patient.BirthDate
                });
            }
            return PatientsObject;
        }

        private void MapDtoToPatient(PatientInsertionDTO patientDto, Patient patient)
        {
            patient.Name = patientDto.Name;
            patient.UserName = patientDto.UserName;
            patient.PhoneNumber = patientDto.PhoneNumber;
            patient.BirthDate = patientDto.BirthDate;
            patient.Email = patientDto.Email;
            patient.PasswordHash = patientDto.Password;
        }
    }
}
