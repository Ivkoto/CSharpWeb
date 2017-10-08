namespace StudentSystem.Client
{
    using StudentSystem.EntityDataModels;
    using System;
    using System.Linq;

    public class DatabaseRequests
    {
        public void MakeRequest(SystemDbContext db)
        {
            //PrintStudentsWithHomeworks(db);
            //PrintCourcesWithResourses(db);
            PrintCoursesWithMoreThan5Resources(db);
        }

        private void PrintCoursesWithMoreThan5Resources(SystemDbContext db)
        {
            var courses = db
                .Courses
                .Where(c => c.Resources.Count > 5)
                .OrderByDescending(c => c.Resources.Count)
                .ThenByDescending(c => c.StartDate)
                .Select(c => new
                {
                    c.Name,
                    ResourceCount = c.Resources.Count
                }).ToList();

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Name} - resourses number: {course.ResourceCount}");                
            }
        }

        private void PrintCourcesWithResourses(SystemDbContext db)
        {
            var courses = db.
                Courses
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new
                {
                    c.Name,
                    c.Description,
                    Resource = c.Resources.Select(r => new
                    {
                        r.Name,
                        r.ResourceType,
                        r.URL
                    })
                })
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine(course.Name);
                Console.WriteLine($"Description: {course.Description}");
                foreach (var resource in course.Resource)
                {
                    Console.WriteLine($"   {resource.Name}: {resource.ResourceType} / {resource.URL}");
                }
            }
        }

        private void PrintStudentsWithHomeworks(SystemDbContext db)
        {
            var students = db
                .Students
                .Select(s => new
                {
                    s.Name,
                    Homeworks = s.Homeworks.Select(h => new
                    {
                        h.Content,
                        h.ContentType
                    })
                }).ToList();
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Name}:");
                foreach (var homework in student.Homeworks)
                {
                    Console.WriteLine($"   {homework.Content} - {homework.ContentType}");
                }
            }
        }
    }
}