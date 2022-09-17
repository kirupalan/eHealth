using System.ComponentModel.DataAnnotations;

namespace eHealthAPI.Models.Domain
{
    public class Medicine
    {
        public Guid Id { get; set; }
        public string Name { get; set; }     
        public string Manufacturer { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public string Disease { get; set; }
        public string Uses { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; }
    }
}