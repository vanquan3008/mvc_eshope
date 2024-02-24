using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_Shopping.Data.Validation;

namespace Web_Shopping.Models
{
	public class ProductModel
	{
		[Key]
		public int Id {  get;set; }
		[Required, MinLength(4, ErrorMessage = "Require input name Product")]
		public string Name { get; set; }
		[Required, MinLength(4, ErrorMessage = "Require input description Product")]
		public string Description { get; set; }
		public string Slug { get; set; }
		public decimal Price { get; set; }
		public int CategoryID { get; set; }
		public int BrandID { get; set;  }
		public CategoryModel Category { get; set; }
		
		public BrandModel Brand { get; set;  }
		public string image { get; set; } = "noimage.jpg";
		[NotMapped]
		[FileExtension]
		public IFormFile imageUpload { get; set; }
	}
}
