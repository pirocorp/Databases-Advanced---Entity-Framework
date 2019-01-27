namespace Demo
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class XorAttribute : ValidationAttribute
    {
        private readonly string xorTarget;

        public XorAttribute(string xorTarget)
        {
            this.xorTarget = xorTarget;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var targetValue = validationContext.ObjectType
                .GetProperty(this.xorTarget)
                .GetValue(validationContext.ObjectInstance);

            if ((targetValue == null && value != null) ||
                (targetValue != null && value == null))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"One of properties must be null other not null");
        }
    }
}