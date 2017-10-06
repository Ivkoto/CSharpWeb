namespace StudentSystem.Client
{
    using Microsoft.EntityFrameworkCore;
    using StudentSystem.Data;
    using StudentSystem.EntityDataModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SeedDatabase
    {
        private static Random random = new Random();

        public void SeedData(SystemDbContext db)
        {
            const int totalStudents = 25;
            const int totalCourses = 10;
            var currentDate = DateTime.Now;

            //Student
            Console.WriteLine("Adding studets");
            for (int i = 0; i < totalStudents; i++)
            {
                db.Students.Add(new Student
                {
                    Name = $"Student {i}",
                    RegistrationDate = currentDate,
                    Birthday = currentDate.AddYears(-20).AddDays(i),
                    PhoneNumber = 885986062 + i
                });
                Console.Write(".");
            }

            //Courses
            Console.WriteLine();
            Console.WriteLine("Adding Courses");
            var addedCourses = new List<Course>();
            for (int i = 0; i < totalCourses; i++)
            {
                var course = new Course
                {
                    Name = $"Course {i}",
                    Description = $"Course details {i}",
                    StartDate = currentDate.AddDays(i),
                    EndDate = currentDate.AddDays(30 + i)
                };
                db.Courses.Add(course);
                addedCourses.Add(course);
                Console.Write(".");
            }

            db.SaveChanges();

            //Studets in courses
            Console.WriteLine();
            Console.WriteLine("Put students in courses");
            var studetIds = db.Students.Select(s => s.id).ToList();

            for (int i = 0; i < totalCourses; i++)
            {
                var studentsInCourse = random.Next(2, totalStudents / 2);
                var currentCourse = addedCourses[i];
                for (int j = 0; j < studentsInCourse; j++)
                {
                    var studentId = studetIds[random.Next(0, studetIds.Count)];

                    if (!currentCourse.Students.Any(s => s.StudentId == studentId))
                    {
                        currentCourse.Students.Add(new StudentsCoursces
                        {
                            StudentId = studentId
                        });
                    }
                    else
                    {
                        j--;
                    }
                }
                Console.Write(".");

                var types = Enum.GetValues(typeof(ResourceType)).Cast<int>().ToArray();
                currentCourse.Resources.Add(new Resource
                {
                    Name = $"Resours {i}",
                    ResourceType = (ResourceType)types[random.Next(0, types.Count())],
                    URL = $"URL {1}"
                });
            }

            db.SaveChanges();

            //Homeworks
            Console.WriteLine();
            Console.WriteLine("Add some homeworks");

            for (int i = 0; i < totalCourses; i++)
            {
                var currentCourse = addedCourses[i];
                var studentsInCurrentCourseIds = currentCourse
                    .Students
                    .Select(s => s.StudentId)
                    .ToList();

                for (int j = 0; j < studentsInCurrentCourseIds.Count; j++)
                {
                    var totalHomeworks = random.Next(2, 5);
                    for (int h = 0; h < totalHomeworks; h++)
                    {
                        db.Homeworks.Add(new Homework
                        {
                            Content = $"Content homework {i}",
                            SubmissionDate = currentDate.AddDays(-i),
                            ContentType = ContentType.zip,
                            CourseId = currentCourse.Id,
                            StudentId = studentsInCurrentCourseIds[j]
                        });
                        Console.Write(".");
                    }
                }
                db.SaveChanges();
            }
            Console.WriteLine();
        }
    }
}
