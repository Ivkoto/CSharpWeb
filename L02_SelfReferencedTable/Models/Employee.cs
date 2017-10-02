namespace L03_SelfReferencedTable.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int? MenagerId { get; set; }
        public Employee Menager { get; set; }

        public ICollection<Employee> Subordinates { get; set; } = new List<Employee>();
    }
}