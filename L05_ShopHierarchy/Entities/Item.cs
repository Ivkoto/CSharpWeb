using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace L05_ShopHierarchy.Entities
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public ICollection<OrdersItems> Orders { get; set; } = new List<OrdersItems>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}