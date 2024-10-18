using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalSystemAPI.Data;
using HospitalSystemAPI.Models;
using HospitalSystemAPI.DTOs.SpecialityDTOs;
using Microsoft.AspNetCore.Authorization;

namespace HospitalSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialityController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public SpecialityController(HospitalDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles ="genAdmin")]
        // GET: api/Speciality
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetSpecialities()
        {
            List<string> specialitiesObject = new List<string>();
            var specialities = await _context.Specialities.ToListAsync();

            foreach (var speciality in specialities)
            {
                specialitiesObject.Add(speciality.Name);
            }
            return specialitiesObject;
        }

        [Authorize(Roles ="genAdmin")]
        // POST: api/Speciality
        [HttpPost]
        public async Task<ActionResult> PostSpeciality([FromBody] SpecialityDTO specialityDto)
        {
            // Validate the incoming data
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the speciality name already exists
            var exists = await _context.Specialities.AnyAsync(s => s.Name == specialityDto.Name);
            if (exists)
            {
                return BadRequest("Speciality name already exists.");
            }

            // Create the new speciality
            var speciality = new Speciality
            {
                Name = specialityDto.Name
            };

            _context.Specialities.Add(speciality);
            await _context.SaveChangesAsync();

            return Ok("Speciality created successfully");
        }

    }
}
