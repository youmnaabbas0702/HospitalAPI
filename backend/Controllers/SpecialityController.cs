using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalSystemAPI.Data;
using HospitalSystemAPI.Models;

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

        // GET: api/Speciality
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Speciality>>> GetSpecialities()
        {
            List<Speciality> specialitiesObject = new List<Speciality>();
            var specialities = await _context.Specialities.Include(s => s.Doctors).ToListAsync();

            foreach (var speciality in specialities)
            {
                if(!(speciality.Name == "General" || speciality.Name == "Emergency"))
                {
                    specialitiesObject.Add(speciality);
                }
            }
            return specialitiesObject;
        }

        //// GET: api/Speciality/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Speciality>> GetSpeciality(int id)
        //{
        //    var speciality = await _context.Specialities.Include(s=>s.Doctors).SingleOrDefaultAsync(s=>s.Id==id);

        //    if (speciality == null)
        //    {
        //        return NotFound();
        //    }

        //    return speciality;
        //}

        // POST: api/Speciality
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Speciality>> PostSpeciality(Speciality speciality)
        {
            _context.Specialities.Add(speciality);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpeciality", new { id = speciality.Id }, speciality);
        }

        // DELETE: api/Speciality/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpeciality(int id)
        {
            var speciality = await _context.Specialities.FindAsync(id);
            if (speciality == null)
            {
                return NotFound();
            }

            _context.Specialities.Remove(speciality);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecialityExists(int id)
        {
            return _context.Specialities.Any(e => e.Id == id);
        }
    }
}
