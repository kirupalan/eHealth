using System.ComponentModel.DataAnnotations;

namespace eHealthAPI.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public int Fund { get; set; }
        public int Account { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}