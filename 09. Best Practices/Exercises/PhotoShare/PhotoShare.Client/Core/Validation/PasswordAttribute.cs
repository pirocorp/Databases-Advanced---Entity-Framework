﻿namespace PhotoShare.Client.Core.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class PasswordAttribute : ValidationAttribute
    {
        private const string SPECIAL_SYMBOLS = "!@#$%^&*()_+<>,.?";
        private readonly int minLength;
        private readonly int maxLength;

        public PasswordAttribute(int minLength, int maxLength)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        public override bool IsValid(object value)
        {
            var password = value.ToString();

            if (password.Length < this.minLength || password.Length > this.maxLength)
            {
                return false;
            }

            if (!password.Any(c => char.IsLower(c)))
            {
                return false;
            }

            //if (!password.Any(c => char.IsUpper(c)))
            //{
            //    return false;
            //}

            if (!password.Any(c => char.IsDigit(c)))
            {
                return false;
            }

            if (password.Any(c => SPECIAL_SYMBOLS.Contains(c)))
            {
                return false;
            }

            return true;
        }
    }
}
