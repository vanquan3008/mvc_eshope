using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_Shopping.Data;
using Web_Shopping.Models;

namespace Web_Shopping.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly DataContext _datacontext;
		public CheckoutController(DataContext data)
		{
			_datacontext = data;
		}
		public async Task<IActionResult> Checkout()
		{
			var UserEmail = User.FindFirstValue(ClaimTypes.Email);
			if( UserEmail == null)
			{
				return RedirectToAction("Login", "Account");
			}
			else
			{
				var orderCode = Guid.NewGuid().ToString();
				var orderItem = new OrderModel();
				orderItem.OrderCode = orderCode;
				orderItem.UserName = UserEmail;
				orderItem.CreateAt = DateTime.Now;
				orderItem.Status = 1;
				_datacontext.Order.Add(orderItem);
				
				List<CartModel> cartList = HttpContext.Session.GetJson<List<CartModel>>("Cart");
				foreach(var cart in cartList)
				{
					var orderDetail = new OrderDetails();
					orderDetail.OrderCode = orderCode;
					orderDetail.UserName = UserEmail;
					orderDetail.ProductId = cart.ProductID;
					orderDetail.Price = cart.Price;
					orderDetail.Quantity = cart.Quantity;
					_datacontext.Add(orderDetail);
				}
				_datacontext.SaveChanges();
				TempData["success"] = "Create Order detail is successfully";
				return RedirectToAction("index", "Home");

			}
			return View();
		}
	}
}
