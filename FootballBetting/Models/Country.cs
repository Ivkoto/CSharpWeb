namespace FootballBetting.Models
{
    using System.ComponentModel.DataAnnotations;
    using FootballBetting.Attributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Country
    {
        [Key]
        [TreeLetterValidator]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public ICollection<Town> Towns { get; set; }

        public ICollection<CountriesContinets> Continents { get; set; }
    }
}