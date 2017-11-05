namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Continent
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<CountriesContinets> Countries { get; set; } = new List<CountriesContinets>();
    }
}