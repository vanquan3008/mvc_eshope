using Microsoft.AspNetCore.Identity;

namespace Web_Shopping.Models
{
	public class AppUserModel :IdentityUser
	{
		public string Occuration { get; set; }
	}
}
