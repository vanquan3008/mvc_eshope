using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using Web_Shopping.Data;
using Web_Shopping.Models;

namespace Web_Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {

        private readonly DataContext _data;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(DataContext data ,IWebHostEnvironment webHostEnvironment) 
        {
            _data = data;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _data.Products.OrderByDescending(p => p.Id).Include("Category").Include("Brand").ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Category = new SelectList(_data.Categories , "Id_Category", "Name");
            ViewBag.Brand = new SelectList(_data.Brands, "Id_Brand", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel product)
        {
            ViewBag.Category = new SelectList(_data.Categories, "Id_Category", "Name" , product.Category);
            ViewBag.Brand = new SelectList(_data.Brands, "Id_Brand", "Name" , product.Brand);

            if (ModelState.IsValid)
            { 
                product.Slug = product.Name.Replace(" ","-");
                var Prod = await _data.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);

                if (Prod != null)
                {
                    ModelState.AddModelError("", "Product is Valid");
                    return View();
                }
                if (product.imageUpload != null)
                {
                    string UploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                    string filename = Guid.NewGuid().ToString() + product.imageUpload.FileName;
                    string fileLoad = Path.Combine(UploadDir, filename);

                    FileStream fs = new FileStream(fileLoad, FileMode.Create);
                    await product.imageUpload.CopyToAsync(fs);
                    fs.Close();

                    product.image = filename;
                }
                _data.Products.Add(product);
                _data.SaveChanges();
                TempData["success"] = "Add product to list successfuly";
                return RedirectToAction("index");
            }
            else
            {
                TempData["error"] = "Model is error";
                List<string> Error = new List<string>();
                foreach(var value in ModelState.Values) {
                   foreach(var err in value.Errors)
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
            ProductModel product = await _data.Products.FindAsync(Id);
            ViewBag.Category = new SelectList(_data.Categories, "Id_Category", "Name" , product.Category);
            ViewBag.Brand = new SelectList(_data.Brands, "Id_Brand", "Name" ,product.Brand);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id , ProductModel product)
        {
            ViewBag.Category = new SelectList(_data.Categories, "Id_Category", "Name", product.Category);
            ViewBag.Brand = new SelectList(_data.Brands, "Id_Brand", "Name", product.Brand);
            if (ModelState.IsValid)
            {
                product.Slug = product.Name.Replace(" ","-");
                var Prod = await _data.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
                if (product.imageUpload != null)
                {
                    string UploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                    string filename = Guid.NewGuid().ToString() + product.imageUpload.FileName;
                    string fileLoad = Path.Combine(UploadDir, filename);

                    FileStream fs = new FileStream(fileLoad, FileMode.Create);
                    await product.imageUpload.CopyToAsync(fs);
                    fs.Close();

                    product.image = filename;
                }
                _data.Products.Update(product);
                _data.SaveChanges();
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
            var product = await _data.Products.FindAsync(Id);
            if (!string.Equals(product.image, "noimage.jpg"))
            {
                string UploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
         
                string fileLoad = Path.Combine(UploadDir, product.image);
                if (System.IO.File.Exists(fileLoad))
                {
                    System.IO.File.Delete(fileLoad);
                }
            }

            _data.Products.Remove(product);
            _data.SaveChanges();
            TempData["success"] = "Product is delete successfully";
            return RedirectToAction("index");
        }
    }
}
