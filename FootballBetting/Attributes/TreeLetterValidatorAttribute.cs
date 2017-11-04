namespace FootballBetting.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class TreeLetterValidatorAttribute : ValidationAttribute
    {
        public TreeLetterValidatorAttribute()
        {
            base.ErrorMessage = "It must contains 3 capital letters!";
        }

        public override bool IsValid(object value)
        {
            var letters = value as string;

            if (letters.Any(l => char.IsLower(l)))
            {
                return false;
            }
            if (letters.Length < 3 || letters.Length > 3)
            {
                return false;
            }

            return true;
        }
    }
}