﻿namespace StudentSystem.EntityDataModels
{
    using System.ComponentModel.DataAnnotations;

    public class License
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int ResourseId { get; set; }

        public Resource Resource { get; set; }
    }
}