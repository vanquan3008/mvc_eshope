using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Shopping.Data;
using Web_Shopping.Models;

namespace Web_Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BrandController : Controller
    {
        private readonly DataContext _data;
        public BrandController(DataContext data)
        {
            _data = data;
        }
        public async Task<IActionResult> Index()
        {

            return View( await _data.Brands.OrderByDescending(p=>p.Id_Brand).ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandModel brand)
        {
            if (ModelState.IsValid)
            {
                brand.Slug = brand.Name.Replace(" ", "-");
                var br = await _data.Brands.FirstOrDefaultAsync(p => p.Slug == brand.Slug);
                if(br != null)
                {
                    TempData["error"] = "The brand is valid";
                    return View(brand);
                }
                _data.Brands.Add(brand);
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
            BrandModel Brand = await _data.Brands.FindAsync(Id);
            return View(Brand);
        }
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BrandModel brand, int Id)
        {
            if (ModelState.IsValid)
            {
                brand.Id_Brand = Id;
                brand.Slug = brand.Name.Replace(" ", "-");
                var Prod = await _data.Brands.FirstOrDefaultAsync(p => p.Slug == brand.Slug);

                if (Prod != null)
                {
                    TempData["error"] = "Category is valid";
                    return View(brand);
                }
                _data.Brands.Update(brand);
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
            BrandModel brand = await _data.Brands.FirstOrDefaultAsync(p => p.Id_Brand == Id);
            if(brand == null)
            {
                return RedirectToAction("index");
            }
            _data.Brands.Remove(brand);
            await _data.SaveChangesAsync();
            return RedirectToAction("index");
		}
    }
}
