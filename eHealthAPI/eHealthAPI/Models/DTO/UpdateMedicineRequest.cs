namespace eHealthAPI.Models.DTO
{
    public class UpdateMedicineRequest
    {
        public string MedicineName { get; set; }
        public string Manufacturer { get; set; }
        public int UnitPrice { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public string Disease { get; set; }
        public string Uses { get; set; }
        public DateTime ExpDate { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
    }
}
