
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
        // GET: api/DoctorSchedule/{doctorId}
        [HttpGet("{doctorId}")]
        public async Task<ActionResult<List<DoctorScheduleDTO>>> GetDoctorSchedule(string doctorId)
        {
            var schedules = await _context.DoctorsSchedules
                .Where(s => s.DoctorId == doctorId)
                .ToListAsync();

            if (schedules == null || schedules.Count == 0)
            {
                return NotFound("No schedules found for this doctor.");
            }

            var scheduleObjects = schedules.Select(schedule => new DoctorScheduleDTO
            {
                Day = schedule.Day.ToString(),
                StartTime = schedule.StartTime.ToString(@"hh\:mm\:ss"), // Format TimeSpan to "HH:mm:ss"
                EndTime = schedule.EndTime.ToString(@"hh\:mm\:ss")      // Format TimeSpan to "HH:mm:ss"
            }).ToList();

            return Ok(scheduleObjects);
        }

        [Authorize(Roles = "genAdmin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorScheduleDTO>> UpdateDoctorSchedule(int id, [FromBody] UpdateScheduleTimeDTO updateScheduleDto)
        {
            // Find the existing schedule by its ID
            var existingSchedule = await _context.DoctorsSchedules.FindAsync(id);

            if (existingSchedule == null)
            {
                return NotFound("Schedule not found");
            }

            // Parse the StartTime and EndTime from string to TimeSpan
            if (!TimeSpan.TryParse(updateScheduleDto.StartTime, out TimeSpan startTime))
            {
                return BadRequest("Invalid Start Time format. Please use HH:mm:ss.");
            }

            if (!TimeSpan.TryParse(updateScheduleDto.EndTime, out TimeSpan endTime))
            {
                return BadRequest("Invalid End Time format. Please use HH:mm:ss.");
            }

            // Update only the StartTime and EndTime
            existingSchedule.StartTime = startTime;
            existingSchedule.EndTime = endTime;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok("Schedule updated successfully");
        }



        [Authorize(Roles = "genAdmin")]
        [HttpPost]
        public async Task<ActionResult<DoctorSchedule>> PostDoctorSchedule(AddScheduleDTO scheduleDto)
        {
            // Try to parse the time strings into TimeSpan
            if (!TimeSpan.TryParse(scheduleDto.StartTime, out TimeSpan startTime))
            {
                return BadRequest("Invalid Start Time format. Please use HH:mm:ss.");
            }

            if (!TimeSpan.TryParse(scheduleDto.EndTime, out TimeSpan endTime))
            {
                return BadRequest("Invalid End Time format. Please use HH:mm:ss.");
            }

            // Create a new DoctorSchedule object
            var doctorSchedule = new DoctorSchedule
            {
                DoctorId = scheduleDto.DoctorId,
                Day = Enum.Parse<DayOfWeek>(scheduleDto.Day), // Ensure Day is a valid DayOfWeek enum
                StartTime = startTime,
                EndTime = endTime
            };

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
    }
}
