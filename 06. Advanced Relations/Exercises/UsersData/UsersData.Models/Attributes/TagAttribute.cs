namespace UsersData.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class TagAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueAsString = (string) value;

            if (valueAsString[0] != '#')
            {
                return new ValidationResult($"Tag must start with '#'.");
            }

            foreach (var ch in valueAsString)
            {
                if (char.IsWhiteSpace(ch))
                {
                    return new ValidationResult($"Tag can not contain white spaces.");
                }
            }

            return ValidationResult.Success;
        }
    }
}