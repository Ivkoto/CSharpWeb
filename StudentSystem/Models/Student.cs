namespace StudentSystem.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Student
    {
        public int id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? PhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime? Birthday { get; set; }

        public ICollection<StudentsCoursces> Cources { get; set; } = new List<StudentsCoursces>();

        public ICollection<Homework> Homeworks { get; set; } = new List<Homework>();
    }
}