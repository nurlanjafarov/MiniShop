namespace MiniShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public DateTime CreatedTime { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }
}
