namespace AcTester.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using Helpers.Utilities;

    public class ModelAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueAsString = (string)value;
            if (valueAsString.Length < Constants.ModelMinLength || valueAsString.Trim() == string.Empty)
            {
                return
                    new ValidationResult(string.Format(Constants.IncorrectPropertyLength, "Model",
                        Constants.ModelMinLength));
            }

            return ValidationResult.Success;
        }
    }
}
