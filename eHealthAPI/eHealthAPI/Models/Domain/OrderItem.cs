namespace eHealthAPI.Models.Domain
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string MedicineName { get; set; }
        public int UnitPrice { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }
}