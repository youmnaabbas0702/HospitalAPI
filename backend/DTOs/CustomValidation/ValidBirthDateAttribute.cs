using System.ComponentModel.DataAnnotations;

namespace HospitalSystemAPI.DTOs.CustomValidation
{
    public class ValidBirthDateAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime birthDate)
            {
                var minDate = new DateTime(1920, 1, 1);
                var maxDate = DateTime.Today;

                if (birthDate < minDate)
                {
                    return new ValidationResult($"Birth date cannot be before {minDate.ToShortDateString()}.");
                }

                if (birthDate > maxDate)
                {
                    return new ValidationResult("Birth date cannot be in the future.");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid birth date.");
        }

    }
}
