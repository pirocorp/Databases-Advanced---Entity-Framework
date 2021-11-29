namespace TeisterMask.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    public class EndDateValidationAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "End date cannot be prior to start date";

        public EndDateValidationAttribute(
            string startDateParamName, 
            string endDateParamName, 
            string dateTimeFormat, 
            bool allowNullEndDate = false)
            : base(DefaultErrorMessage)
        {
            this.StartDatePropertyName = startDateParamName;
            this.EndDatePropertyName = endDateParamName;
            this.DateTimeFormat = dateTimeFormat;
            this.AllowNullEndDate = allowNullEndDate;
        }

        private string StartDatePropertyName { get; set; }

        private string EndDatePropertyName { get;  set; }

        private string DateTimeFormat { get; set; }

        private bool AllowNullEndDate { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var startDate = this.GetPropertyValue(this.StartDatePropertyName, validationContext);
            var endDate = this.GetPropertyValue(this.EndDatePropertyName, validationContext);

            if (!startDate.HasValue)
            {
                throw new ArgumentNullException(nameof(this.StartDatePropertyName));
            }

            if (this.AllowNullEndDate && !endDate.HasValue)
            {
                return ValidationResult.Success;
            }

            if (!endDate.HasValue)
            {
                throw new ArgumentNullException(nameof(this.EndDatePropertyName));
            }

            if (startDate.Value < endDate.Value)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(DefaultErrorMessage);
        }


        private DateTime? GetPropertyValue(string propertyName, ValidationContext validationContext)
        {
            var containerType = validationContext.ObjectInstance.GetType();
            var field = containerType.GetProperty(propertyName);
            var extensionValue = field.GetValue(validationContext.ObjectInstance);

            if (extensionValue == null)
            {
                return null;
            }

            var dataType = extensionValue.GetType();

            if (dataType == this.DateTimeFormat.GetType())
            {
                var dateAsString = (string)extensionValue;

                if (string.IsNullOrWhiteSpace(dateAsString))
                {
                    return null;
                }

                return DateTime.ParseExact(dateAsString, this.DateTimeFormat, CultureInfo.InvariantCulture);
            }

            return (DateTime?)extensionValue;
        }
    }
}
