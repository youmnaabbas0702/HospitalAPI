﻿using HospitalSystemAPI.Models;

namespace HospitalSystemAPI.DTOs.PatientDTOs
{
    public class GetPatientDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        //public MedicalHistory[] MedicalHistories { get; set; } = default!;
    }
}
