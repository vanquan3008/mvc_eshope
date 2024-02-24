using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Shopping.Data;

namespace Web_Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize]
	public class OrderDetailsController : Controller
	{
        private readonly DataContext _data;

        public OrderDetailsController(DataContext data)
        {
            _data = data;
        }
        public async Task<IActionResult> Index()
        {

            return View(await _data.Order.OrderByDescending(c => c.Id).ToListAsync());
        }
        public async Task<IActionResult> View(string OrderCode)
        {
            var detailProd = await _data.OrderDetails.Include(o => o.Product).Where(o => o.OrderCode == OrderCode).ToListAsync();
            return View(detailProd);
        }
    }
}
