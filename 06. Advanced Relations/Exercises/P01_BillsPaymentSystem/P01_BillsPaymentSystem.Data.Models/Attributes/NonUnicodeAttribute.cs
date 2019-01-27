namespace P01_BillsPaymentSystem.Data.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class NonUnicodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var nullErrorMessage = "Value can not be null";
            var unicodeErrorMessage = "Value can not contain unicode characters";

            if (value == null)
            {
                return new ValidationResult(nullErrorMessage);
            }

            var text = (string) value;

            for (var i = 0; i < text.Length; i++)
            {
                if (text[i] > 255)
                {
                    return new ValidationResult(unicodeErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}