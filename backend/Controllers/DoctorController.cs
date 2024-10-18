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
using System.Numerics;
using HospitalSystemAPI.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Authorization;
using HospitalSystemAPI.DTOs.DoctorDTOs;

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
        [Authorize(Roles = "genAdmin")]
        // GET: api/Doctor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDoctorDTO>>> GetDoctors()
        {
            var doctors = await _context.Doctors.Include(d => d.Speciality).ToListAsync();

            List<GetDoctorDTO> DoctorsObject = getDoctorsDTOobject(doctors);
            return DoctorsObject;
        }

        //used with general admin authorization
        [Authorize(Roles = "genAdmin")]
        // GET: api/Doctor/Speciality/5
        [HttpGet("Speciality/{id}")]
        public async Task<ActionResult<IEnumerable<GetDoctorDTO>>> GetSpecialityDoctors(int id)
        {
            var doctors = await _context.Doctors.Where(d => d.SpecialityId == id).ToListAsync();

            List<GetDoctorDTO> DoctorsObject = getDoctorsDTOobject(doctors);
            return DoctorsObject;
        }

        //used with admin or doctor authorization
        [Authorize(Roles = "genAdmin,Doctor")]
        // GET: api/Doctor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetDoctorDTO>> GetDoctor(string id)
        {
            var doctor = await _context.Doctors.Include(d => d.Speciality).SingleOrDefaultAsync(d => d.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            GetDoctorDTO DoctorObject = new GetDoctorDTO()
            {
                Name = doctor.Name,
                Email = doctor.Email,
                speciality = doctor.Speciality.Name,
                PhoneNumber = doctor.PhoneNumber,
            };
            return DoctorObject;
        }

        //with general admin authorization
        [Authorize(Roles = "genAdmin")]
        // PUT: api/Doctor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(string id, DoctorEditDTO doctorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            doctor.PhoneNumber = doctorDto.PhoneNumber;
            doctor.UserName = doctorDto.UserName;
            doctor.Email = doctorDto.Email;
            doctor.Email = doctorDto.Email;
            doctor.PasswordHash = doctorDto.Password;

            _context.Entry(doctor).State = EntityState.Modified;

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

        //with general admin authorization
        [Authorize(Roles = "genAdmin")]
        // DELETE: api/Doctor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(string id)
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

        private bool DoctorExists(string id)
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
                    Name = doctor.Name,
                    Email = doctor.Email,
                    speciality = doctor.Speciality?.Name ?? " ",
                    PhoneNumber = doctor.PhoneNumber,
                });
            }
            return DoctorsObject;
        }

        private void MapDtoToDoctor(DoctorInsertionDTO doctorDto, Doctor doctor)
        {
            
            doctor.Name = doctorDto.Name;
            doctor.PhoneNumber = doctorDto.PhoneNumber;
            doctor.SpecialityId = doctorDto.SpecialityId;
            doctor.UserName = doctorDto.UserName;
            doctor.Email = doctorDto.Email;
            doctor.Email = doctorDto.Email;
            doctor.PasswordHash = doctorDto.Password;
        }
    }

}
