namespace SocialNetwork.Data.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordValidatorAttribute : ValidationAttribute
    {
        private readonly char[] AllowedSymbols = new[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '<', '>', '?' };

        public PasswordValidatorAttribute()
        {
            base.ErrorMessage = $"Password should contain at least: " +
                 $"1 lowercase letter, " +
                 $"1 uppercase letter, " +
                 $"1 digit, " +
                 $"1 special symbol (!, @, #, $, %, ^, &, *, (, ), _, +, <, >, ?)";
        }

        public override bool IsValid(object value)
        {
            var password = value as string;

            if (password == null)
            {
                return true;
            }

            var validationExpression = @"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=()_<>?\[\]]).*$";
            Regex regex = new Regex(validationExpression);
            return regex.IsMatch(password);
        }
    }
}