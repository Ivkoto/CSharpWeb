namespace BankSystem.Models.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordValidatorAttribute : ValidationAttribute
    {
        public PasswordValidatorAttribute()
        {
            base.ErrorMessage = "Password must be at lest 6 characters and it must have letters, numbers and symbols";
        }

        public override bool IsValid(object value)
        {
            var password = value as string;

            return password.Any(char.IsLetter) && password.Any(char.IsDigit) && password.Any(char.IsSymbol);            
        }
    }
}