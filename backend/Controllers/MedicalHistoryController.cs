using HospitalSystemAPI.Data;
using HospitalSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HospitalSystemAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalHistoryController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public MedicalHistoryController(HospitalDbContext context)
        {
            _context = context;
        }

        // 1. Retrieve Medical History (Doctor or Patient)
        [HttpGet("{patientId}")]
        [Authorize(Roles = "Doctor, Patient")] // Only accessible by Doctors and Patients
        public async Task<IActionResult> GetMedicalHistory(string patientId)
        {
            // The authorized user can either be a doctor or the specific patient.
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Check if the user is a Patient and trying to access their own history
            if (User.IsInRole("Patient") && currentUserId != patientId)
            {
                return Forbid("You are not authorized to view another patient's medical history.");
            }

            var history = await _context.MedicalHistories
                .Where(mh => mh.PatientId == patientId)
                .ToListAsync();

            if (history == null || history.Count == 0)
            {
                return NotFound("No medical history found for this patient.");
            }

            return Ok(history);
        }

        // 2. Add Medical History (Doctors Only)
        [HttpPost]
        [Authorize(Roles = "Doctor")] // Only accessible by Doctors
        public async Task<IActionResult> AddMedicalHistory(MedicalHistory newHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the patient exists
            var patientExists = await _context.Patients
                .AnyAsync(p => p.Id == newHistory.PatientId);

            if (!patientExists)
            {
                return NotFound(new { Message = "Patient does not exist" });
            }

            // Create new medical history entry
            var medicalHistory = new MedicalHistory
            {
                PatientId = newHistory.PatientId,
                Condition = newHistory.Condition,
                RecordDate = newHistory.RecordDate,
                Treatment = newHistory.Treatment
            };

            _context.MedicalHistories.Add(medicalHistory);
            await _context.SaveChangesAsync();

            return Ok("Medical history added successfully.");
        }
    }


}

