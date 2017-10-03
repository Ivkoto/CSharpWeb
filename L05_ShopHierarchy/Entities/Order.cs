using L05_ShopHierarchy.Models;
using System.Collections.Generic;

namespace L05_ShopHierarchy.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<OrdersItems> Items { get; set; } = new List<OrdersItems>();
    }
}