namespace eHealthAPI.Models.Domain
{
    public class Order_Item
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MedicineName { get; set; }
        public int UnitPrice { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }
}