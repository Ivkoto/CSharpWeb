namespace SocialNetwork.Data.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class TagValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var tag = value as string;
            if (tag == null)
            {
                return true;
            }

            return tag.StartsWith("#") && tag.All(e => !char.IsWhiteSpace(e));
        }
    }
}