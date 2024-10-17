using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalSystemAPI.Data;
using HospitalSystemAPI.Models;
using HospitalSystemAPI.DTOs.DoctorDTOs;
using Microsoft.AspNetCore.Authorization;

namespace HospitalSystemAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public DoctorScheduleController(HospitalDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "genAdmin,Doctor")]
        // GET: api/DoctorSchedule/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<DoctorScheduleDTO>>> GetDoctorSchedules(string id)
        {
            var doctorSchedule = await _context.DoctorsSchedules.Include(s => s.Doctor).Where(s => s.DoctorId==id).ToListAsync();

            if (doctorSchedule == null)
            {
                return NotFound();
            }

            var ScheduleObject = new List<DoctorScheduleDTO>();

            foreach(var schedule in doctorSchedule)
            {
                ScheduleObject.Add(new DoctorScheduleDTO() { DoctorName = schedule.Doctor.Name, Day = schedule.Day, StartTime = schedule.StartTime, EndTime = schedule.EndTime });
            }
            return ScheduleObject;
        }

        [Authorize(Roles = "genAdmin")]
        // PUT: api/DoctorSchedule/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctorSchedule(int id, DoctorSchedule doctorSchedule)
        {
            if (id != doctorSchedule.Id)
            {
                return BadRequest();
            }

            _context.Entry(doctorSchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorScheduleExists(id))
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

        [Authorize(Roles = "genAdmin")]
        // POST: api/DoctorSchedule
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DoctorSchedule>> PostDoctorSchedule(DoctorSchedule doctorSchedule)
        {
            _context.DoctorsSchedules.Add(doctorSchedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctorSchedule", new { id = doctorSchedule.Id }, doctorSchedule);
        }

        [Authorize(Roles = "genAdmin")]
        // DELETE: api/DoctorSchedule/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctorSchedule(int id)
        {
            var doctorSchedule = await _context.DoctorsSchedules.FindAsync(id);
            if (doctorSchedule == null)
            {
                return NotFound();
            }

            _context.DoctorsSchedules.Remove(doctorSchedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorScheduleExists(int id)
        {
            return _context.DoctorsSchedules.Any(e => e.Id == id);
        }
    }
}
