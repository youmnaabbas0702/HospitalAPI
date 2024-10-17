using HospitalSystemAPI.Data;
using HospitalSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HospitalSystemAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HospitalSystemAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
       
            private readonly HospitalDbContext _context;

            public MedicalRecordsController(HospitalDbContext context)
            {
                _context = context;
            }

            [HttpPost]
            public async Task<IActionResult> AddMedicalRecord(int AppointmentId,MedicalRecordDto model)
            {
                var patient = await _context.Patients.FindAsync(model.PatientId);
                if (patient == null)
                {
                    return NotFound("Patient not found");
                }

                var doctor = await _context.Doctors.FindAsync(model.DoctorId);
                if (doctor == null)
                {
                    return NotFound("Doctor not found");
                }

                // Create new Medical Record
                var medicalRecord = new MedicalRecord
                {
                    PatientId = model.PatientId,
                    DoctorId = model.DoctorId,
                    VisitDate = DateTime.Now,
                    Diagnosis = model.Diagnosis,
                    Prescription = model.Prescription,
                    AddToMedicalHistory = model.AddToMedicalHistory
                };

                _context.MedicalRecords.Add(medicalRecord);

                // Check if the doctor chooses to add it to the medical history
                if (model.AddToMedicalHistory)
                {
                    var medicalHistory = new MedicalHistory
                    {
                        PatientId = model.PatientId,
                        Condition = model.Diagnosis,
                        RecordDate = DateTime.Now,
                        Treatment = model.Prescription
                    };
                    _context.MedicalHistories.Add(medicalHistory);
                }

                // Save changes
                await _context.SaveChangesAsync();

                // Delete appointment since the visit is completed
                var appointment = await _context.Appointments
                    .Where(a=>a.Id == AppointmentId)
                    .FirstOrDefaultAsync();

                if (appointment != null)
                {
                    _context.Appointments.Remove(appointment);
                    await _context.SaveChangesAsync();
                }

                return Ok("Medical record added successfully");
            }

        [HttpGet]
        public async Task<IActionResult> GetAllMedicalRecords()
        {
            var records = await _context.MedicalRecords.ToListAsync();
            return Ok(records);
        }

        //show record for patient by id for patient
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicalRecordById(int id)
        {
            var record = await _context.MedicalRecords.FindAsync(id);
            if (record == null)
            {
                return NotFound("Medical record not found");
            }

            return Ok(record);
        }

        //Delete record for patient by id for patient
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalRecord(int id)
        {
            var record = await _context.MedicalRecords.FindAsync(id);
            if (record == null)
            {
                return NotFound("Medical record not found");
            }

            _context.MedicalRecords.Remove(record);
            await _context.SaveChangesAsync();

            return Ok("Medical record deleted successfully");
        }
    }

      
    }
