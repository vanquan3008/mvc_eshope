using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Web_Shopping.Data;
using Web_Shopping.Models;

namespace Web_Shopping.Controllers
{
    public class CategoryController:Controller
    {
        private readonly DataContext _data;
        public CategoryController (DataContext data)
        {
            _data = data;      
        }
        public async Task<IActionResult> Index(string Slug = "")
        {
            CategoryModel category = _data.Categories.Where(p => p.Slug == Slug).FirstOrDefault();
            if(category == null)
            {
                return RedirectToAction("index");
            }
            var product = _data.Products.Where(p => p.Category.Id_Category == category.Id_Category);

            return View(await product.OrderByDescending(c=>c.CategoryID).ToListAsync());
        }
        
    }
}
