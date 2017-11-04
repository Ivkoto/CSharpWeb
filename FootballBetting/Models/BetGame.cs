namespace FootballBetting.Models
{
    using FootballBetting.Enum;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BetGame
    {
        public int GameId { get; set; }
        
        public Game Game { get; set; }
        
        public int BetId { get; set; }
        
        public Bet Bet { get; set; }

        public int ResultPredictionId { get; set; }

        public ResultPrediction ResultPrediction { get; set; }
    }
}