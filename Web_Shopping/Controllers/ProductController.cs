using Microsoft.AspNetCore.Mvc;
using Web_Shopping.Data;
using Web_Shopping.Models;

namespace Web_Shopping.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _data;
        public ProductController(DataContext data)
        {
            _data = data;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int id) { 
            if(id == null){
                return RedirectToAction("index");
            }
            ProductModel product = _data.Products.Where(p => p.Id == id).FirstOrDefault();
            return View(product);
        }
    }
}
