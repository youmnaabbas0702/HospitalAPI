using HospitalSystemAPI.DTOs.PatientDTOs;
using HospitalSystemAPI.Models;

namespace HospitalSystemAPI.DTOs.AppointmentDTOs
{
    public class NewAppointment
    {
        //public bool PatientExixst;
        public int PatientId {  get; set; }
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int DoctorId { get; set; } 
        //public PatientInsertionDTO PatientInsertionDTO { get; set; } = default;
    }
}
