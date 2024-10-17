using HospitalSystemAPI.Data;
using HospitalSystemAPI.DTOs.AppointmentDTOs;
using HospitalSystemAPI.DTOs.PatientDTOs;
using HospitalSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // 1. Get All Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewAppointments>>> GetAppointments()
        {
            var appointment = _context.Appointments.ToList();
            List<ViewAppointments> viewlist=new List<ViewAppointments>();
            foreach(var item in appointment)
            {
                var app = new ViewAppointments
                {
                    PatientId = item.PatientId,
                    AppointmentId = item.Id,
                    AppointmentDate = item.AppointmentDate,
                    DoctorId = item.DoctorId,

                };
                viewlist.Add(app);
            }
            return Ok(viewlist);

        }

        // 2. Get Appointment by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        // 3. Add New Appointment
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(NewAppointment newappointment)
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patientExists = await _context.Patients
                .AnyAsync(p => p.Id == newappointment.PatientId);

            if (!patientExists)
            {
                return NotFound(new { Message = "Patient does not exist" });
            }

            var doctorExists = await _context.Doctors
                .AnyAsync(d => d.Id == newappointment.DoctorId);

            if (!doctorExists)
            {
                return NotFound(new { Message = "Doctor does not exist" });
            }

            
            var appointment = new Appointment
            {
                Id = newappointment.AppointmentId,
                PatientId = newappointment.PatientId,
                DoctorId = newappointment.DoctorId,
                AppointmentDate = newappointment.AppointmentDate
            };

            
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

           
            return Ok("Created Successfuly");
        
        //appointmentDTO.PatientExixst = exist;
        //if (appointmentDTO.PatientExixst)
        //{
        //    var patient = _context.Patients.FirstOrDefault(p => p.Id == appointmentDTO.PatientId);
        //    if (patient == null)
        //    {
        //        return BadRequest("Not Found");

        //    }
        //    else
        //    {
        //        patient.Appointments.Add(new Appointment());
        //        return Ok("Added ");
        //    }
        //}
        //Create a new appointment
        //var newAppointment = new Appointment
        //{
        //    Id = appointment.AppointmentId,
        //    AppointmentDate = appointment.AppointmentDate,
        //    Doctor =_context.Doctors.FirstOrDefault(d=>d.Id == appointment.DoctorId),
        //    Patient=_context.Patients.FirstOrDefault(p=>p.Id==appointment.PatientId),
        //};

        //_context.Appointments.Add(newAppointment);
        //await _context.SaveChangesAsync();

        //    return Ok("Created");
        }

        // 4. Get Appointments by Doctor ID
        [HttpGet("byDoctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByDoctor(string doctorId)
        {
            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
        }

        // 5. Get Appointments by Patient ID
        [HttpGet("byPatient/{patientId}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByPatient(string patientId)
        {
            return await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .ToListAsync();
        }
        // 6. Update an Appointment
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAppointment(int id, [FromBody] JsonPatchDocument<Appointment> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(appointment);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           
            await _context.SaveChangesAsync();
            
           

            return NoContent();
        }


        // 7. Delete an Appointment
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(d=>d.Id==id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return Ok("Deleted Sucsses ");

        }
    }
}

