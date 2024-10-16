using HospitalSystemAPI.Data;

namespace HospitalSystemAPI.Services
{
    public class IdGenerator : IIdGenerator
    {
        private readonly HospitalDbContext _context;

        public IdGenerator(HospitalDbContext context)
        {
            _context = context;
        }

        public string GenerateDoctorId()
        {
            // Fetch the latest DoctorId
            var lastDoctor =  _context.Doctors
                .OrderByDescending(d => d.Id)
                .FirstOrDefault();

            if (lastDoctor != null)
            {
                // Extract numeric part and increment
                var lastNumber = int.Parse(lastDoctor.Id.Substring(3));
                var newNumber = lastNumber + 1;
                return $"DOC{newNumber:D4}";
            }
            else
            {
                // Start from DOC0001
                return "DOC0001";
            }
        }

        public string GeneratePatientId()
        {
            // Fetch the latest PatientId
            var lastPatient = _context.Patients
                .OrderByDescending(p => p.Id)
                .FirstOrDefault();

            if (lastPatient != null)
            {
                // Extract numeric part and increment
                var lastNumber = int.Parse(lastPatient.Id.Substring(3));
                var newNumber = lastNumber + 1;
                return $"PAT{newNumber:D4}";
            }
            else
            {
                // Start from PAT0001
                return "PAT0001";
            }
        }
    }
}
