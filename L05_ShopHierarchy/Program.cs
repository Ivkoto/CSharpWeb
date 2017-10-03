namespace L05_ShopHierarchy
{
    using L05_ShopHierarchy.Entities;
    using L05_ShopHierarchy.Models;
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
                AddItems(db);
                ProcessCommand(db);
                //PrintSalesmanWithCustomerCount(db);
                //PrintCustomersWithOrdersAndReviewsCount(db);
                //PrintCustomerOrdersAndReviews(db);
                //PrintCustomerInfo(db);
                PrintOrdersWithMoreThanOneItem(db);
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

        private static void AddItems(ShopDbContext db)
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "END")
                {
                    break;
                }

                var itemTokens = input.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var itemName = itemTokens[0];
                var itemPrice = decimal.Parse(itemTokens[1]);

                db.Add(new Item() { Name = itemName, Price = itemPrice });
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
            var currentOrder = new Order() { CustomerId = customerId };

            foreach (var itemId in argumets.Skip(1))
            {
                currentOrder.Items.Add(new OrdersItems
                {
                    ItemId = int.Parse(itemId),
                });
            }

            db.Add(currentOrder);

            db.SaveChanges();
        }

        private static void SaveReview(ShopDbContext db, string[] argumets)
        {
            var customerId = int.Parse(argumets[0]);
            var itemId = int.Parse(argumets[1]);

            db.Add(new Review()
            {
                CustomerId = customerId,
                ItemId = itemId
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

        private static void PrintCustomerOrdersAndReviews(ShopDbContext db)
        {
            var customerId = int.Parse(Console.ReadLine().Trim());
            
            var customerData = db
                .Customers
                .Where(c => c.Id == customerId)
                .Select(c => new
                {
                    Orders = c.Orders.Select(o => new
                    {
                        o.Id,
                        ItemCount = o.Items.Count
                    })
                    .OrderBy(o => o.Id),
                    Reviews = c.Reviews.Count
                })
                .FirstOrDefault();

            foreach (var order in customerData.Orders)
            {
                Console.WriteLine($"order{order.Id}: {order.ItemCount} items");
            }
            Console.WriteLine($"reviews: {customerData.Reviews}");
        }

        private static void PrintCustomerInfo(ShopDbContext db)
        {
            var customerId = int.Parse(Console.ReadLine());

            var customerData = db
                .Customers
                .Where(c => c.Id == customerId)
                .Select(c => new
                {
                    c.Name,
                    OrdersCount = c.Orders.Count,
                    ReviewsCount = c.Reviews.Count,
                    SalesmanName = c.Salesman.Name
                })
                .FirstOrDefault();

            Console.WriteLine($"Customer: {customerData.Name}");
            Console.WriteLine($"Orders count: {customerData.OrdersCount}");
            Console.WriteLine($"Reviews: {customerData.ReviewsCount}");
            Console.WriteLine($"Salesman: {customerData.SalesmanName}");
        }


        private static void PrintOrdersWithMoreThanOneItem(ShopDbContext db)
        {
            var customerId = int.Parse(Console.ReadLine());

            var orders = db
                .Orders
                .Where(o => o.CustomerId == customerId)
                .Where(o => o.Items.Count > 1)
                .Count();
        }
    }
}