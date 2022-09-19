namespace eHealthAPI.Models.DTO
{
    public class UpdateOrderRequest
    {
        public int UserId { get; set; }
        public int OrderNumber { get; set; }
        public int OrderTotal { get; set; }
        public string OrderStatus { get; set; }
    }
}