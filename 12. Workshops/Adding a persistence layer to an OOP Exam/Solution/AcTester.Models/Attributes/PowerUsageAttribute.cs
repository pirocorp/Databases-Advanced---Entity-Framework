namespace AcTester.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using Helpers.Utilities;

    public class PowerUsageAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var originalValue = (int)value;
            if (originalValue <= 0)
            {
                return new ValidationResult(string.Format(Constants.NonPositiveNumber, "Power Usage"));
            }
            return ValidationResult.Success;
        }
    }
}
