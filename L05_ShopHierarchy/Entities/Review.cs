using L05_ShopHierarchy.Models;

namespace L05_ShopHierarchy.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}