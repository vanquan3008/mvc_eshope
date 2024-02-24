using System.ComponentModel.DataAnnotations;

namespace Web_Shopping.Models
{
	public class UserModel
	{
		[Key]
		public int Id { get; set; }
		[Required , MinLength(6, ErrorMessage = "Username must be larger than 6 characters")]
		public string Username { get; set; }
		[Required, MinLength(4, ErrorMessage = "Password must be larger than 4 characters"),DataType(DataType.Password)]
		public string Password { get; set; }
		[Required,EmailAddress]
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
	}
}
