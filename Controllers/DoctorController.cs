using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalSystemAPI.Data;
using HospitalSystemAPI.Models;
using HospitalSystemAPI.DTOs;

namespace HospitalSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public DoctorController(HospitalDbContext context)
        {
            _context = context;
        }

        //used with general admin authorization
        // GET: api/Doctor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDoctorDTO>>> GetDoctors()
        {
            var doctors = await _context.Doctors.Include(d => d.Speciality).ToListAsync();

            List<GetDoctorDTO> DoctorsObject = getDoctorsDTOobject(doctors);
            return DoctorsObject;
        }

        //used with speciality and general admin authorization
        // GET: api/Doctor/Speciality/5
        [HttpGet("Speciality/{id}")]
        public async Task<ActionResult<IEnumerable<GetDoctorDTO>>> GetSpecialityDoctors(int id)
        {
            var doctors = await _context.Doctors.Include(d => d.Speciality).Where(d => d.SpecialityId == id).ToListAsync();

            List<GetDoctorDTO> DoctorsObject = getDoctorsDTOobject(doctors);
            return DoctorsObject;
        }

        //used with admin or doctor authorization
        // GET: api/Doctor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetDoctorDTO>> GetDoctor(int id)
        {
            var doctor = await _context.Doctors.Include(d => d.Speciality).SingleOrDefaultAsync(d => d.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            GetDoctorDTO DoctorObject = new GetDoctorDTO()
            {
                Id = doctor.Id,
                Name = doctor.Name,
                PhoneNumber = doctor.PhoneNumber,
                speciality = doctor.Speciality.Name
            };
            return DoctorObject;
        }

        //with general and specialization admin authorization
        // PUT: api/Doctor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, DoctorInsertionDTO doctorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDoctor = await _context.Doctors.FindAsync(id);
            if (existingDoctor == null)
            {
                return NotFound();
            }

            // Call the private method to map DTO to the entity
            MapDtoToDoctor(doctorDto, existingDoctor);

            _context.Entry(existingDoctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        //with general and specialization admin authorization
        // POST: api/Doctor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostDoctor(DoctorInsertionDTO doctorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Create a new Doctor entity
            var newDoctor = new Doctor();

            // Call the private method to map DTO to the entity
            MapDtoToDoctor(doctorDto, newDoctor);

            _context.Doctors.Add(newDoctor);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DoctorExists(newDoctor.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDoctor", new { id = newDoctor.Id }, newDoctor);
        }

        //with general and specialization admin authorization
        // DELETE: api/Doctor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }

        private List<GetDoctorDTO> getDoctorsDTOobject(List<Doctor> doctors)
        {
            List<GetDoctorDTO> DoctorsObject = new List<GetDoctorDTO>();
            foreach (var doctor in doctors)
            {
                DoctorsObject.Add(new GetDoctorDTO()
                {
                    Id = doctor.Id,
                    Name = doctor.Name,
                    PhoneNumber = doctor.PhoneNumber,
                    speciality = doctor.Speciality.Name
                });
            }
            return DoctorsObject;
        }

        private void MapDtoToDoctor(DoctorInsertionDTO doctorDto, Doctor doctor)
        {
            doctor.Id = doctorDto.Id;
            doctor.Name = doctorDto.Name;
            doctor.PhoneNumber = doctorDto.PhoneNumber;
            doctor.SpecialityId = doctorDto.SpecialityId;
            doctor.Email = doctorDto.Email;
            doctor.password = doctorDto.Password;
        }
    }

}
