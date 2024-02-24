using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Shopping.Models;

namespace Web_Shopping.Data.Component
{
	public class BrandViewComponent : ViewComponent
	{
		private readonly DataContext _data;


		public BrandViewComponent(DataContext data)
		{
			_data = data;
		}
		public async Task<IViewComponentResult> InvokeAsync() => View(await _data.Brands.ToListAsync());
	}
}
