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
            //PrintCoursesWithMoreThan5Resources(db);
            //PrintCoursesOnGivenDate(db);
            //PrintStudentsWithPricesPerCourse(db);
            //PrintCoursesWithResources(db);
            PrintStudentsWithCoursesResourcesAndLicenses(db);
        }

        private void PrintStudentsWithCoursesResourcesAndLicenses(SystemDbContext db)
        {
            var result = db
                .Students
                .Where(s => s.Cources.Any())
                .Select(s => new
                {
                    s.Name,
                    Courses = s.Cources.Count,
                    Resources = s.Cources.Sum(c => c.Course.Resources.Count),
                    Licenses = s.Cources.Sum(c => c.Course.Resources.Sum(r => r.Licenses.Count()))
                }).ToList();

            foreach (var student in result)
            {
                Console.WriteLine(student.Name);
                Console.WriteLine($"  Total courses: {student.Courses} ");
                Console.WriteLine($"  Total resources: {student.Resources}");
                Console.WriteLine($"  Total licenses: {student.Licenses}");
            }
        }

        private void PrintCoursesWithResources(SystemDbContext db)
        {
            var result = db
                .Courses
                .OrderByDescending(c => c.Resources.Count())
                .ThenBy(c => c.Name)
                .Select(c => new
                {
                    c.Name,
                    Resources = c.Resources
                        .OrderByDescending(r => r.Licenses.Count)
                        .ThenBy(r => r.Name)
                        .Select(r => new
                        {
                            r.Name,
                            Licenses = r.Licenses.Select(l => l.Name)
                        })
                }).ToList();

            foreach (var course in result)
            {
                Console.WriteLine(course.Name);
                foreach (var resource in course.Resources)
                {
                    Console.WriteLine($"--Resource: {resource.Name}");
                    Console.WriteLine($"--Licenses:");
                    Console.WriteLine("   " + string.Join(",", resource.Licenses));
                }
                Console.WriteLine(new string('-', 10));
            }
        }

        private void PrintStudentsWithPricesPerCourse(SystemDbContext db)
        {
            var result = db
                .Students
                .Where(s => s.Cources.Any())
                .Select(s => new
                {
                    s.Name,
                    Courses = s.Cources.Count,
                    TotalPrice = s.Cources.Sum(c => c.Course.Price),
                    AveragePrice = s.Cources.Average(c => c.Course.Price)
                })
                .OrderByDescending(s => s.TotalPrice)
                .ThenByDescending(s => s.Courses)
                .ThenBy(s => s.Name)
                .ToList();

            foreach (var student in result)
            {
                Console.WriteLine($"{student.Name}");
                Console.WriteLine($"Courses: {student.Courses}");
                Console.WriteLine($"Average price by course: {student.AveragePrice:f2}");
                Console.WriteLine($"Total prise: {student.TotalPrice:f2}");
                Console.WriteLine(new string('-', 10));
            }
        }

        private void PrintCoursesOnGivenDate(SystemDbContext db)
        {
            var givenDate = DateTime.Parse(Console.ReadLine());

            var courses = db
                .Courses
                .Where(c => c.StartDate < givenDate && givenDate < c.EndDate)
                .Select(c => new
                {
                    c.Name,
                    c.StartDate,
                    c.EndDate,
                    Duration = c.EndDate.Subtract(c.StartDate),
                    StudentsCout = c.Students.Count
                })
                .OrderByDescending(c => c.StudentsCout)
                .ThenByDescending(c => c.Duration)
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine(course.Name);
                Console.WriteLine($"  start date: {course.StartDate.ToShortDateString()}");
                Console.WriteLine($"  end date: {course.EndDate.ToShortDateString()}");
                Console.WriteLine($"  duration: {course.Duration.TotalDays} days");
                Console.WriteLine($"  students number: {course.StudentsCout}");
            }
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