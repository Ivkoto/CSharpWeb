namespace SocialNetwork.Data.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property)]
    internal class EmailValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var email = value as string;

            var pattern = @"^[a-zA-Z0-9][a-zA-Z0-9-._]+[a-zA-Z0-9]@([a-zA-Z]+\.)[a-zA-Z]+$";

            return Regex.IsMatch(email, pattern);
        }
    }
}