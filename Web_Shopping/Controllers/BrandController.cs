using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Shopping.Data;
using Web_Shopping.Models;

namespace Web_Shopping.Controllers
{
	public class BrandController:Controller
	{
		private readonly DataContext _data;
		public BrandController (DataContext data)
		{
			_data = data;
		}
		public async Task<IActionResult> Index(string slug="")
		{
			BrandModel brand = _data.Brands.Where(p => p.Slug == slug).FirstOrDefault();
			if(brand == null)
			{
				return RedirectToAction("index");
			}
			var Product = _data.Products.Where(p => p.BrandID == brand.Id_Brand);
			return View(await Product.OrderByDescending(p=>p.Id).ToListAsync());
		}
	}
}
