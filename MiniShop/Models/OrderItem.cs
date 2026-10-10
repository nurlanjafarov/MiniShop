namespace MiniShop.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public int TicketId { get; set; }

        public Ticket Ticket { get; set; } = null!;
        public Order Order { get; set; } = null!;
    }
}
