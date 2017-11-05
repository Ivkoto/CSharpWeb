namespace FootballBetting.Logic
{
    public class TreeLetterTransformer
    {
        public static string Transform(string value)
        {
            var result = value.Replace(" ", string.Empty);

            if (result.Length > 3)
            {
                result = result.Substring(0, 3);
            }

            return result.ToUpper();
        }
    }
}