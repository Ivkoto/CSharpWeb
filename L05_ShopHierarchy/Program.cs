using L05_ShopHierarchy.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;

namespace L05_ShopHierarchy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new ShopDbContext())
            {
                PrepareDatabase(context);
                AddSalesman(context);
                ProcessCommand(context);
                PrintSalesmanWithCustomerCount(context);
            }
        }

        private static void PrepareDatabase(ShopDbContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }

        private static void AddSalesman(ShopDbContext db)
        {
            var salesmanNames = Console.ReadLine().Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var name in salesmanNames)
            {
                var currentSalesman = new Salesman() { Name = name };
                db.Add(currentSalesman);
            }
            db.SaveChanges();
        }

        private static void ProcessCommand(ShopDbContext db)
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "END")
                {
                    break;
                }

                var registerTokens = input.Split(new[] { '-',}, StringSplitOptions.RemoveEmptyEntries);
                var command = registerTokens[0];
                var argumets = registerTokens[1].Split(new[] { ';', }, StringSplitOptions.RemoveEmptyEntries);

                switch (command)
                {
                    case "register":
                        RegisterCustomer(db, argumets);
                        break;

                    default:
                        break;
                }
            }
        }

        private static void RegisterCustomer(ShopDbContext db, string[] argumets)
        {
            var customerName = argumets[0];
            var salesmanId = int.Parse(argumets[1]);
            db.Add(new Customer()
            {
                Name = customerName,
                SalesmanId = salesmanId
            });
            db.SaveChanges();
        }        

        private static void PrintSalesmanWithCustomerCount(ShopDbContext db)
        {
            var salesmanData = db
                .Salesmans
                .Select(s => new
                {
                    s.Name,
                    CustomersCount = s.Customers.Count
                })
                .OrderByDescending(s => s.CustomersCount)
                .ThenBy(s => s.Name)
                .ToArray();

            foreach (var salesmen in salesmanData)
            {
                Console.WriteLine($"{salesmen.Name} - {salesmen.CustomersCount} customers");
            }
        }
    }
}