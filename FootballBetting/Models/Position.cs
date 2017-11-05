namespace FootballBetting.Models
{
    using FootballBetting.Attributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Position
    {
        [Key]
        [TwoLetterValidator]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}