namespace FootballBetting.Models
{
    using FootballBetting.Enum;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ResultPrediction
    {
        public int Id { get; set; }

        [Required]
        public Preduction Preduction { get; set; }

        public ICollection<BetGame> BetGames { get; set; }
    }
}