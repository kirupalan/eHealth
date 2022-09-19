namespace eHealthAPI.Models.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public int Fund { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}
