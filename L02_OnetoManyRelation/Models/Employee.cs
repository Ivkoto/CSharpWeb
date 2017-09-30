using System.ComponentModel.DataAnnotations;

namespace L02_OnetoManyRelation.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public Department Department { get; set; }
    }
}