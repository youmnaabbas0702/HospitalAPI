using HospitalSystemAPI.DTOs.PatientDTOs;
using HospitalSystemAPI.Models;

namespace HospitalSystemAPI.DTOs.AppointmentDTOs
{
    public class NewAppointment
    {
        //public bool PatientExixst;
        public string PatientId {  get; set; }
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string DoctorId { get; set; } 
        //public PatientInsertionDTO PatientInsertionDTO { get; set; } = default;
    }
}
