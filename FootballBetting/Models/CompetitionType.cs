namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CompetitionType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Competition> Competitions { get; set; } = new List<Competition>();
    }
}