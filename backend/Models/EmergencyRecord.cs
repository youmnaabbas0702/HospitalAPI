using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSystemAPI.Models
{
    public class EmergencyRecord
    {
        public int Id { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int ShiftId { get; set; }

        [ForeignKey("ShiftId")]
        public EmergencySchedule Shift { get; set; } = default!;
    }
}
