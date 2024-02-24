using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web_Shopping.Data;
using Web_Shopping.Models;

namespace Web_Shopping.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _data;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger ,DataContext data )
        {
            _logger = logger;
            _data = data;
        }
        public IActionResult Index()
        {
            var product = _data.Products.Include("Category").Include("Brand").ToList();
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statuscode)
        {
            if (statuscode == 404)
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            return View("NotFound");
        }
    }
}
