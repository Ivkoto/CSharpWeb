namespace BankSystem.Models.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property)]
    internal class UsernameValidatorAttribute : ValidationAttribute
    {
        public UsernameValidatorAttribute()
        {
            base.ErrorMessage = $"Username must start with letter and can containt letters, numbers and some symbols(-, _)";
        }

        public override bool IsValid(object value)
        {
            var username = value as string;

            if (username == null)
            {
                return false;
            }

            var validationPattern = @"^[A-Za-z][A-Za-z0-9_-]{2,}$";
            Regex regex = new Regex(validationPattern);
            return regex.IsMatch(username);
        }
    }
}