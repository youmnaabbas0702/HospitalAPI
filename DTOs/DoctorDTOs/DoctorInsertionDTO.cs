using System.ComponentModel.DataAnnotations;

namespace HospitalSystemAPI.DTOs
{
    public class DoctorInsertionDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Name must be between 10 and 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^(01[0-2]\d{8}|0\d{1,2}\d{7})$", ErrorMessage = "Please write a valid Egyptian phone or mobile number.")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Age number is required")]
        public int Age { get; set; }

        public int SpecialityId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = string.Empty;
    }
}
