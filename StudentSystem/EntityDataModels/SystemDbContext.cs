namespace StudentSystem.EntityDataModels
{
    using Microsoft.EntityFrameworkCore;

    public class SystemDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<License> Licenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MyTempDB;Integrated Security=True;");
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<StudentsCoursces>(entity =>
                {
                    entity
                        .HasKey(sc => new { sc.StudentId, sc.CourseId });
                    entity
                        .HasOne(sc => sc.Student)
                        .WithMany(s => s.Cources)
                        .HasForeignKey(sc => sc.StudentId)
                        .HasConstraintName("FK_StudentCourses_Student_StudentId");
                    entity
                        .HasOne(sc => sc.Course)
                        .WithMany(c => c.Students)
                        .HasForeignKey(sc => sc.CourseId)
                        .HasConstraintName("FK_StudentsCourses_Course_CourseId");
                });

            builder
                .Entity<Resource>(entity =>
                {
                    entity
                        .HasOne(r => r.Course)
                        .WithMany(c => c.Resources)
                        .HasForeignKey(r => r.CourseId)
                        .HasConstraintName("FK_Resources_Course_CourseId");
                });

            builder
                .Entity<Homework>(entity =>
                {
                    entity
                        .HasOne(h => h.Course)
                        .WithMany(c => c.Homeworks)
                        .HasForeignKey(h => h.CourseId)
                        .HasConstraintName("FK_Homework_Course_CourseId");

                    entity
                        .HasOne(h => h.Student)
                        .WithMany(s => s.Homeworks)
                        .HasForeignKey(h => h.StudentId)
                        .HasConstraintName("FK_Homework_Student_StudentId");
                });

            builder
                .Entity<License>()
                .HasOne(l => l.Resource)
                .WithMany(r => r.Licenses)
                .HasForeignKey(l => l.ResourseId)
                .HasConstraintName("FK_License_Resourse_ResourseId");
        }
    }
}