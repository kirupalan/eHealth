using System.ComponentModel.DataAnnotations;

namespace eHealthAPI.Models.Domain
{
    public class Auth
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}