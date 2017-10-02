using L05_ShopHierarchy.Models;

namespace L05_ShopHierarchy.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}