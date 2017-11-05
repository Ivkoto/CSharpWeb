namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Color
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Team> PrimaryTeams { get; set; } = new List<Team>();

        public ICollection<Team> SecondaryTeams { get; set; } = new List<Team>();
    }
}