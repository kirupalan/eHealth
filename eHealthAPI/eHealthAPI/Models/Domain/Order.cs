namespace eHealthAPI.Models.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrderNumber { get; set; }
        public int OrderTotal { get; set; }
        public string OrderStatus { get; set; }
    }
}
