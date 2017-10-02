namespace L04_ManyToManyRelation
{
    using L04_ManyToManyRelation.Models;
    using Microsoft.EntityFrameworkCore;

    public class MyDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<CoursesStudets> CoursesStudents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=LENOVO-KOSTOV;Database=MyTempDB;Integrated Security=True");
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<CoursesStudets>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            //builder
            //    .Entity<Student>()
            //    .HasMany(s => s.Courses)
            //    .WithOne(sc => sc.Student)
            //    .HasForeignKey(sc => sc.StudentId);

            //builder
            //    .Entity<Course>()
            //    .HasMany(c => c.Students)
            //    .WithOne(sc => sc.Course)
            //    .HasForeignKey(sc => sc.CourseId);

            //2nd way
            //builder
            //    .Entity<CoursesStudets>()
            //    .HasOne(sc => sc.Student)
            //    .WithMany(s => s.Courses)
            //    .HasForeignKey(sc => sc.StudentId);

            //builder
            //    .Entity<CoursesStudets>()
            //    .HasOne(sc => sc.Course)
            //    .WithMany(c => c.Students)
            //    .HasForeignKey(sc => sc.CourseId);

            //3rd way
            builder.Entity<CoursesStudets>(entity =>
            {
                entity
                    .HasOne(sc => sc.Student)
                    .WithMany(s => s.Courses)
                    .HasForeignKey(sc => sc.StudentId)
                    .HasConstraintName("FK_CoursesStudents_Student_StudentId");

                entity
                    .HasOne(sc => sc.Course)
                    .WithMany(c => c.Students)
                    .HasForeignKey(sc => sc.CourseId)
                    .HasConstraintName("FK_CoursesStudets_Course_CourseId");
            });
        }
    }
}