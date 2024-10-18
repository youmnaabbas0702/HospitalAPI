using HospitalSystemAPI.Data;
using HospitalSystemAPI.DTOs.AppointmentDTOs;
using HospitalSystemAPI.DTOs.PatientDTOs;
using HospitalSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HospitalSystemAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public AppointmentController(HospitalDbContext context)
        {
            _context = context;
        }

        // 1. Get All Appointments (Admin Only, ordered by nearest to furthest)
        [HttpGet]
        [Authorize(Roles = "genAdmin")] // Allow only Admins
        public async Task<ActionResult<IEnumerable<ViewAppointments>>> GetAppointments()
        {
            var appointment = await _context.Appointments
                .OrderBy(a => a.AppointmentDate)  // Ordered by date
                .ToListAsync();

            List<ViewAppointments> viewlist = new List<ViewAppointments>();

            foreach (var item in appointment)
            {
                var app = new ViewAppointments
                {
                    PatientId = item.PatientId,
                    AppointmentDate = item.AppointmentDate,
                    DoctorId = item.DoctorId,
                };
                viewlist.Add(app);
            }
            return Ok(viewlist);
        }

        // 2. Get Appointment by ID (Admin/Doctor for their own appointments)
        [HttpGet("{id}")]
        [Authorize(Roles = "genAdmin,Doctor")]  // Only Admin or Doctor
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a=>a.Doctor)
                .FirstOrDefaultAsync(a=>a.Id == id);
                ;
            if (appointment == null) return NotFound();

            // Restrict doctor to their own appointments
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Doctor") && appointment.DoctorId != userId)
            {
                return Forbid();  // Doctor can only view their appointments
            }

            var appointmentObject = new ViewAppointments()
            {
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate,
                DoctortName = appointment.Doctor.Name,
                PatientName = appointment.Patient.Name
            };
            return Ok(appointmentObject);
        }

        // 3. Add New Appointment (Admin only)
        [HttpPost]
        [Authorize(Roles = "genAdmin")]  // Only Admin can create appointments
        public async Task<ActionResult<Appointment>> PostAppointment(NewAppointment newappointment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if the patient exists
            var patientExists = await _context.Patients
                .AnyAsync(p => p.Id == newappointment.PatientId);

            if (!patientExists)
                return NotFound(new { Message = "Patient does not exist" });

            // Check if the doctor exists
            var doctorExists = await _context.Doctors
                .AnyAsync(d => d.Id == newappointment.DoctorId);

            if (!doctorExists)
                return NotFound(new { Message = "Doctor does not exist" });

            // Get the day of the week from the appointment date
            var appointmentDayOfWeek = newappointment.AppointmentDate.DayOfWeek;

            // Check if the doctor has a schedule on the specified day
            var doctorSchedule = await _context.DoctorsSchedules
                .FirstOrDefaultAsync(s => s.DoctorId == newappointment.DoctorId && s.Day == appointmentDayOfWeek);

            if (doctorSchedule == null)
                return BadRequest(new { Message = "Doctor is not available on the selected day" });

            // Check if the doctor is already booked for another appointment at the same date
            var existingAppointment = await _context.Appointments
                .AnyAsync(a => a.DoctorId == newappointment.DoctorId && a.AppointmentDate.Date == newappointment.AppointmentDate.Date);

            if (existingAppointment)
                return BadRequest(new { Message = "Doctor is already booked for another appointment on this date" });

            // Proceed to create the new appointment
            var appointment = new Appointment
            {
                PatientId = newappointment.PatientId,
                DoctorId = newappointment.DoctorId,
                AppointmentDate = newappointment.AppointmentDate
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return Ok("Appointment created successfully");
        }


        // 4. Get Appointments by Doctor ID (Doctor can only access their appointments)
        [HttpGet("byDoctor/{doctorId}")]
        [Authorize(Roles = "genAdmin,Doctor")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByDoctor(string doctorId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Doctor") && doctorId != userId) return Forbid();  // Doctors can only view their appointments

            var appointments = await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Include(a=>a.Patient)
                .Include(a=>a.Doctor)
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();
            var appointmentsObject = new List<ViewAppointments>();
            foreach (var appointment in appointments)
            {
                appointmentsObject.Add(new ViewAppointments
                {
                    PatientId = appointment.PatientId,
                    DoctorId = appointment.DoctorId,
                    AppointmentDate = appointment.AppointmentDate,
                    PatientName = appointment.Patient.Name,
                    DoctortName = appointment.Doctor.Name
                });
            }

            return Ok(appointmentsObject);
        }

        // 5. Get Appointments by Patient ID (Patient can only access their own appointments)
        [HttpGet("byPatient/{patientId}")]
        [Authorize(Roles = "genAdmin,Patient")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByPatient(string patientId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Patient") && patientId != userId) return Forbid();  // Patients can only view their appointments

            var Appointments = await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .Include(a=>a.Patient)
                .Include(a=>a.Doctor)
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();

            var appointmentsObject = new List<ViewAppointments>();
            foreach (var appointment in Appointments)
            {
                appointmentsObject.Add(new ViewAppointments
                {
                    PatientId = appointment.PatientId,
                    DoctorId = appointment.DoctorId,
                    AppointmentDate = appointment.AppointmentDate,
                    PatientName = appointment.Patient.Name,
                    DoctortName = appointment.Doctor.Name
                });
            }

            return Ok(appointmentsObject);
        }

        // 7. Delete an Appointment (Admin Only)
        [HttpDelete("{id}")]
        [Authorize(Roles = "genAdmin")]  // Only Admin can delete appointments
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return Ok("Deleted Successfully");
        }
    }
}

