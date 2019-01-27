namespace P01_BillsPaymentSystem.Data.Models.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

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

            if((value == null && targetValue != null) ||
               (value != null) && targetValue == null)
            {
                return ValidationResult.Success;
            }

            var errorMessage = "One property must be null other not null!";
            return new ValidationResult(errorMessage);
        }
    }
}