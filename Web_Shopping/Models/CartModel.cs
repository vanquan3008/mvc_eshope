using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace Web_Shopping.Models
{
    public class CartModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { 
            get { return Quantity * Price; }
        }
        public string image { get; set; }
        public CartModel()
        {

        }
        public CartModel(ProductModel Product)
        {
            this.ProductID = Product.Id;
            this.ProductName = Product.Name;
            this.Price = Product.Price;
            this.Quantity = 1;
            this.image = Product.image;
        }
    }
}
