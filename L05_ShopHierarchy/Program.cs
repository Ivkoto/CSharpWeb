namespace L05_ShopHierarchy
{
    using L05_ShopHierarchy.Entities;
    using L05_ShopHierarchy.Models;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new ShopDbContext())
            {
                PrepareDatabase(db);
                AddSalesman(db);
                ProcessCommand(db);
                //PrintSalesmanWithCustomerCount(db);
                PrintCustomersWithOrdersAndReviewsCount(db);
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

                var registerTokens = input.Split(new[] { '-', }, StringSplitOptions.RemoveEmptyEntries);
                var command = registerTokens[0];
                var argumets = registerTokens[1].Split(new[] { ';', }, StringSplitOptions.RemoveEmptyEntries);

                switch (command)
                {
                    case "register":
                        RegisterCustomer(db, argumets);
                        break;

                    case "order":
                        CreateOrder(db, argumets);
                        break;

                    case "review":
                        SaveReview(db, argumets);
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

        private static void CreateOrder(ShopDbContext db, string[] argumets)
        {
            var customerId = int.Parse(argumets[0]);
            //db.Add(new Order()
            //{
            //    CustomerId = customerId
            //});
            //db.SaveChanges();

            EntityEntry<Order> currentOrder = db.Add(new Order());
            var currentCustomer = db.Customers.Where(c => c.Id == customerId).FirstOrDefault();
            currentCustomer.Orders.Add(currentOrder.Entity);
        }

        private static void SaveReview(ShopDbContext db, string[] argumets)
        {
            var customerId = int.Parse(argumets[0]);
            db.Add(new Review()
            {
                CustomerId = customerId
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


        private static void PrintCustomersWithOrdersAndReviewsCount(ShopDbContext db)
        {
            var customerData = db
                .Customers
                .Select(c => new
                {
                    c.Name,
                    OrdersCount = c.Orders.Count,
                    ReviewsCount = c.Reviews.Count
                })
                .OrderByDescending(c => c.OrdersCount)
                .ThenByDescending(c => c.ReviewsCount)
                .ToArray();

            foreach (var customer in customerData)
            {
                Console.WriteLine(customer.Name);
                Console.WriteLine($"Orders: {customer.OrdersCount}");
                Console.WriteLine($"Reviews: {customer.ReviewsCount}");
            }
        }
    }
}