﻿namespace eHealthAPI.Models.DTO
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MedicineName { get; set; }
        public int UnitPrice { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }
}
