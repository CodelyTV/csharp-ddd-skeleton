namespace CodelyTv.Shared.Validator.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UuidAttribute : ValidationAttribute
    {
        
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            bool isValid = Guid.TryParse((string) value, out Guid result);

            if (isValid) return ValidationResult.Success;
            
            return new ValidationResult($"The field {value} is not a valid uuid");
        }
    }
}