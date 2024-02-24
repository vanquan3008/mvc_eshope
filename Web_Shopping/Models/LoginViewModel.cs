using System.ComponentModel.DataAnnotations;

namespace Web_Shopping.Models
{
    public class LoginViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(6, ErrorMessage = "Username must be larger than 6 characters")]
        public string Username { get; set; }
        [Required, MinLength(4, ErrorMessage = "Password must be larger than 4 characters"), DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
