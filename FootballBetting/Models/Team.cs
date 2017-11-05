namespace FootballBetting.Models
{
    using FootballBetting.Attributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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

        public ICollection<Player> Players { get; set; } = new List<Player>();

        public ICollection<Game> HomeTeamGames { get; set; } = new List<Game>();

        public ICollection<Game> AwayTeamGames { get; set; } = new List<Game>();
    }
}