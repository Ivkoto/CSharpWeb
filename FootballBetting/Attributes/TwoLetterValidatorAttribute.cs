namespace FootballBetting.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class TwoLetterValidatorAttribute : ValidationAttribute
    {
        public TwoLetterValidatorAttribute()
        {
            base.ErrorMessage = "It must contains 2 capital letters!";
        }

        public override bool IsValid(object value)
        {
            var letters = value as string;

            if (letters.Any(l => char.IsLower(l)))
            {
                return false;
            }
            if (letters.Length < 2 || letters.Length > 2)
            {
                return false;
            }

            return true;
        }
    }
}