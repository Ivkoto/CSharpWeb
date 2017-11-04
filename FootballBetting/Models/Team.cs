namespace FootballBetting.Models
{
    using System.ComponentModel.DataAnnotations;
    using FootballBetting.Attributes;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    public class Team
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public byte? Logo { get; set; }

        [MinLength(3)]
        [MaxLength(3)]
        [TreeLetterValidator]
        public string Initials { get; set; }
                
        public int PrimaryKitColorId { get; set; }
        
        public Color PrimaryKitColor { get; set; }
        
        public int SecondaryKitColorId { get; set; }
        
        public Color SecondaryKitColor { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }
        
        public decimal Budget { get; set; }

        public ICollection<Player> Players { get; set; }

        public ICollection<Game> HomeTeamGames { get; set; }

        public ICollection<Game> AwayTeamGames { get; set; }
    }
}