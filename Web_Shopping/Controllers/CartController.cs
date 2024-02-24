using Microsoft.AspNetCore.Mvc;
using Web_Shopping.Data;
using Web_Shopping.Models;


namespace Web_Shopping.Controllers
{
    public class CartController :Controller
        
    {
        private readonly DataContext _data;
        public CartController (DataContext data)
        {
            _data = data;
        }

        public IActionResult Index()
        {
            List<CartModel> ListCart = HttpContext.Session.GetJson<List<CartModel>>("Cart") ?? new List<CartModel>();

            CartViewModel CartView = new (){
                cartView = ListCart,
                TotalAllCart = ListCart.Sum(p => p.Total)
            };

            return View(CartView);
        }



        public async Task<IActionResult> Add (int Id)
        {
            if (Id == null) return RedirectToAction("index");

            ProductModel product = await _data.Products.FindAsync(Id);

            List<CartModel> cart = HttpContext.Session.GetJson<List<CartModel>>("Cart") ?? new List<CartModel>();

            CartModel cartList = cart.Where(p => p.ProductID == Id).FirstOrDefault();

            if(cartList == null)
            {
                cart.Add(new CartModel(product));

            }
            else
            {
                cartList.Quantity += 1;
            }
            HttpContext.Session.SetJson("Cart" , cart);

            TempData["success"] = "Add product to cart successfully.";
            return Redirect(Request.Headers["Referer"].ToString());            
        }
        public async Task<IActionResult> Decrease(int Id)
        {
            if (Id == null) return RedirectToAction("index");

            List<CartModel> cartList = HttpContext.Session.GetJson<List<CartModel>>("Cart");

            CartModel cart = cartList.Where(p => p.ProductID == Id).FirstOrDefault();

            cart.Quantity -= 1;
            if (cart.Quantity == 0)
            {

                cartList.Remove(cart);
                if (cartList.Count() == 0)
                {
                    HttpContext.Session.Remove("Cart");
                }
                else
                {
                    HttpContext.Session.SetJson("Cart", cartList);
            
                }
            }
            
                
            return RedirectToAction("index");


		}
        public async Task<IActionResult> Increase(int Id)
        {
            if (Id == null) return RedirectToAction("index");

            List<CartModel> cartList = HttpContext.Session.GetJson<List<CartModel>>("Cart");

            CartModel cart = cartList.Where(p => p.ProductID == Id).FirstOrDefault();

            cart.Quantity += 1;

            HttpContext.Session.SetJson("Cart", cartList);
            return RedirectToAction("index");
		}
        public async Task<IActionResult> Remove (int Id)
        {

			if (Id == null) return RedirectToAction("index");

			List<CartModel> cartList = HttpContext.Session.GetJson<List<CartModel>>("Cart");

			CartModel cart = cartList.Where(p => p.ProductID == Id).FirstOrDefault();
            cartList.Remove(cart);
            
            if(cartList.Count() == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
				HttpContext.Session.SetJson("Cart", cartList);
			}
            TempData["success"] = "Delete product to cart successfully.";
            return RedirectToAction("index");
		}
        public async Task<IActionResult> RemoveAll()
        {
            HttpContext.Session.Remove("Cart");
            TempData["success"] = "Delete all product to cart successfully.";
            return RedirectToAction("index");
        }
        public IActionResult Checkout()
        {
            return View("~/Views/Checkout/index.cshtml");
        }
    }
}
