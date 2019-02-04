namespace PhotoShare.Client.Core.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class AgeAttribute : ValidationAttribute
    {
        private const int MIN_AGE = 0;
        private const int MAX_AGE = 150;

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var age = int.Parse(value.ToString());

            return age >= MIN_AGE && age <= MAX_AGE;
        }
    }
}
