namespace L04_ManyToManyRelation.Models
{
    public class CoursesStudets
    {
        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}