namespace eHealthAPI.Models.DTO
{
    public class UpdateCartRequest
    {
        public int UserId { get; set; }
        public string MedicineName { get; set; }
        public int UnitPrice { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }
}
