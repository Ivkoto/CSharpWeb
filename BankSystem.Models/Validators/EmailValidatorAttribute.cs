namespace BankSystem.Models.Validators
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    internal class EmailValidatorAttribute : ValidationAttribute
    {
        public EmailValidatorAttribute()
        {
            base.ErrorMessage = "email must be like example@mail.com and it can contain numbers";
        }

        public override bool IsValid(object value)
        {
            var email = value as string;

            var validationPattern = @"[\w\d_.-]+@[\w\d]+.[\w]+";
            Regex regex = new Regex(validationPattern);

            return regex.IsMatch(email);
        }
    }
}