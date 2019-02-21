namespace AcTester.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using Helpers.Utilities;

    public class ManufacturerAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueAsString = (string)value;
            if (valueAsString.Length < Constants.ManufacturerMinLength || valueAsString.Trim() == string.Empty)
            {
                return new ValidationResult(string.Format(Constants.IncorrectPropertyLength, "Manufacturer", Constants.ManufacturerMinLength));
            }

            return ValidationResult.Success;
        }
    }
}
