namespace L05_ShopHierarchy.Entities
{
    public class OrdersItems
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}