using System.ComponentModel.DataAnnotations;

namespace HospitalSystemAPI.DTOs.SpecialityDTOs
{
    public class SpecialityDTO
    {
        [Required]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "The name must contain only letters.")]
        [StringLength(20, ErrorMessage = "The name cannot exceed 20 characters.")]
        public string Name { get; set; } = string.Empty;
    }

}
