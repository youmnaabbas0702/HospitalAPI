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

namespace HospitalSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public PatientController(HospitalDbContext context)
        {
            _context = context;
        }

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
        public async Task<ActionResult<GetPatientDTO>> GetPatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            GetPatientDTO PatientObject = new GetPatientDTO()
            {
                Id = patient.Id,
                Name = patient.Name,
                PhoneNumber = patient.PhoneNumber,
                BirthDate = patient.BirthDate,
                MedicalHistory = patient.MedicalHistory,
            };
            return PatientObject;
        }

        // PUT: api/Patient/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, PatientInsertionDTO patientDto)
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

            // Call the private method to map DTO to the entity
            MapDtoToPatient(patientDto, existingPatient);

            _context.Entry(existingPatient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Patient
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(PatientInsertionDTO patientDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Create a new patient entity
            var newPatient = new Patient();

            // Call the private method to map DTO to the entity
            MapDtoToPatient(patientDto, newPatient);

            _context.Patients.Add(newPatient);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PatientExists(newPatient.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPatient", new { id = newPatient.Id }, newPatient);
        }

        // DELETE: api/Patient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
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

        private bool PatientExists(int id)
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
                    PhoneNumber = patient.PhoneNumber,
                    BirthDate = patient.BirthDate,
                    MedicalHistory = patient.MedicalHistory,
                });
            }
            return PatientsObject;
        }

        private void MapDtoToPatient(PatientInsertionDTO patientDto, Patient patient)
        {
            patient.Id = patientDto.Id;
            patient.Name = patientDto.Name;
            patient.PhoneNumber = patientDto.PhoneNumber;
            patient.BirthDate = patientDto.BirthDate;
        }
    }
}
