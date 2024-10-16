using HospitalSystemAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            [HttpGet("{patientId}")]
            public async Task<IActionResult> GetMedicalHistory(string patientId)
            {
                var history = await _context.MedicalHistories
                    .Where(mh => mh.PatientId == patientId)
                    .ToListAsync();

                if (history == null || history.Count == 0)
                {
                    return NotFound("No medical history found for this patient");
                }

                return Ok(history);
            }
        }

    }

