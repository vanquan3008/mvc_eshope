using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_Shopping.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web_Shopping.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUserModel> _userManager;
		private readonly SignInManager<AppUserModel> _signinManager;
		public AccountController(UserManager<AppUserModel> userManager, SignInManager<AppUserModel> signinManager)
		{
			_userManager = userManager;
			_signinManager = signinManager;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Login(string returnUrl)
		{
			return View(new LoginViewModel { ReturnUrl = returnUrl });
		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Login(LoginViewModel user)
		{
			if (ModelState.IsValid)
			{
				Microsoft.AspNetCore.Identity.SignInResult result = await _signinManager.PasswordSignInAsync(user.Username, user.Password, false, false);
				if (result.Succeeded)
				{
					return Redirect(user.ReturnUrl ?? "/");
				}
				ModelState.AddModelError("", "Password is not successfully");
				TempData["success"] = "Login successfully";
			}
			else
			{
				List<string> Error = new List<string>();
				foreach (var value in ModelState.Values)
				{
					foreach (var err in value.Errors)
					{
						Error.Add(err.ErrorMessage);
					}
				}
				string errmess = string.Join("/n", Error);

				return BadRequest(errmess);
			}
			
			return View(user);
		}
		public async Task<IActionResult> Register()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(UserModel user)
		{
			if (ModelState.IsValid)
			{
				AppUserModel newuser = new AppUserModel
				{
					UserName = user.Username,
					Email = user.Email
				};
				IdentityResult result = await _userManager.CreateAsync(newuser, user.Password);
				if (result.Succeeded)
				{
					TempData["success"] = " Create user is successfully";
					return Redirect("/account/login");
				}
				else
				{
					foreach (var err in result.Errors)
					{
						ModelState.AddModelError("", err.Description);
					}
					return View(user);
				}
			}
			else
			{
				return View(user);
			}

		}
	
		public async Task<IActionResult> LogOut(string returnUrl = "/")
		{
			await _signinManager.SignOutAsync();
			return Redirect(returnUrl);
	
		}
    }
}
