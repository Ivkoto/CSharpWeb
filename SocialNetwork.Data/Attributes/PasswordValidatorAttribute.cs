namespace SocialNetwork.Data.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordValidatorAttribute : ValidationAttribute
    {
        private readonly char[] AllowedSymbols = new[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '<', '>', '?' };

        public PasswordValidatorAttribute()
        {
            this.ErrorMessage = $"Password should contain at least: " +
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
            return password.Any(s =>
                char.IsLower(s)
                && char.IsUpper(s)
                && char.IsDigit(s)
                && this.AllowedSymbols.Contains(s));
        }
    }
}