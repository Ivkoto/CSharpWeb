using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace L02_OnetoManyRelation.Models
{
    public class Department
    {
        public Department()
        {
            this.Employees = new List<Employee>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}