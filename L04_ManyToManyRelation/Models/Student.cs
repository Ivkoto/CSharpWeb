using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace L04_ManyToManyRelation.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<CoursesStudets> Courses { get; set; } = new List<CoursesStudets>();
    }
}