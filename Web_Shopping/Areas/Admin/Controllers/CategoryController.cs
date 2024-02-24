using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Shopping.Data;
using Web_Shopping.Models;

namespace Web_Shopping.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize]
	public class CategoryController : Controller
	{
		private readonly DataContext _data;

		public CategoryController(DataContext data)
		{
			_data = data;
		}
		public async Task<IActionResult> Index()
		{

			return View(await _data.Categories.OrderByDescending(c=>c.Id_Category).ToListAsync()); 
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CategoryModel category)
		{
            category.Slug = category.Name.ToLower().Replace(" ", "-");

            if (ModelState.IsValid)
			{
				

                var slug = await _data.Categories.FirstOrDefaultAsync(p => p.Slug == category.Slug);
				if (slug != null)
				{
					TempData["error"] = "Category is valid";
					return View(category);
				}

				_data.Add(category);
				await _data.SaveChangesAsync();
				return RedirectToAction("index");

			}
			else
			{
				TempData["error"] = "Model is fail";
       
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
			
		}
        [HttpGet]
		public async Task<IActionResult> Edit(int Id)
		{
			CategoryModel Category = await _data.Categories.FindAsync(Id);
			return View(Category);
		}
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryModel category ,int Id)
        {
            if (ModelState.IsValid)
            {
                category.Id_Category = Id;
                category.Slug = category.Name.Replace(" ", "-");
                var Prod = await _data.Categories.FirstOrDefaultAsync(p => p.Slug == category.Slug);

                if (Prod != null)
                {
                    TempData["error"] = "Category is valid";
                    return View(category);
                }
                _data.Categories.Update(category);
                await _data.SaveChangesAsync();
                TempData["success"] = "Update product to list successfuly";
                return RedirectToAction("index");
            }
            else
            {
                TempData["error"] = "Model is error";
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
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var Category = _data.Categories.FirstOrDefault(p => p.Id_Category == Id);

            _data.Categories.Remove(Category);
            await _data.SaveChangesAsync();
            return RedirectToAction("index");
        }
    }
}