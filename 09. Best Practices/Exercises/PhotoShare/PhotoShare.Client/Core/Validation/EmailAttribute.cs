﻿namespace PhotoShare.Client.Core.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class EmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var email = value as string;

            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            return email.Contains("@");
        }
    }
}
