namespace FootballBetting
{
    using FootballBetting.Data;
    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        public static void Main()
        {
            var db = new FootballBettingContext();
            db.Database.Migrate();
        }
    }
}