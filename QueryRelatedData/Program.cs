using QueryRelatedData.Models;
using System;
using System.Linq;

namespace QueryRelatedData
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new MyDbContext())
            {
                //CreateDepartment(db,"Sales");
                //CreateEmployees(db, 9);
                //db.SaveChanges();

                var department = db
                    .Departments
                    .Where(d => d.Id == 1)
                    .Select(d => new
                    {
                        d.Name,
                        employeeCount = d.Employees.Count
                    })
                    .FirstOrDefault();
            }

            //ClearDatabase(db);
        }

        private static void CreateEmployees(MyDbContext db, int count)
        {
            for (int i = 1; i < count+1; i++)
            {
                db.Employees.Add(new Employee()
                {
                    Name = $"Person-{i}",
                    DepartmentId = 1
                });
            }
        }

        private static void CreateDepartment(MyDbContext db, string name)
        {
            db.Departments.Add(new Department()
            {
                Name = name
            });
        }

        private static void ClearDatabase(MyDbContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}