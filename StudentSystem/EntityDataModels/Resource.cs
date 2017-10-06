namespace StudentSystem.EntityDataModels
{
    using StudentSystem.Data;
    using System.ComponentModel.DataAnnotations;

    public class Resource
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ResourceType ResourceType { get; set; }

        [Required]
        public string URL { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}