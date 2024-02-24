using Microsoft.EntityFrameworkCore;
using Web_Shopping.Models;

namespace Web_Shopping.Data
{
	public class SeedingData
	{
		public static void Seeding(DataContext _context)
		{
			_context.Database.Migrate();
			if (!_context.Products.Any())
			{
				CategoryModel macbook = new CategoryModel { Name = "macbook", Description = "macbook is a big", Slug = "macbook", Status = 1 };
				CategoryModel pc = new CategoryModel { Name = "pc", Description = "pc is a big", Slug = "pc", Status = 1 };

				BrandModel apple = new BrandModel { Name = "Apple", Description = "Morderm ", Slug = "iphone", Status = 1 };
				BrandModel samsung = new BrandModel { Name = "Samsung", Description = "Mordermest asian ", Slug = "samsung", Status = 1 };
				_context.Products.AddRange([
					new ProductModel
					{
						Name = "Macbookm1",
						Description = "Macbook is the best",
						Slug = "macbookm1",
						image = "./1.jpg",
						Category = macbook,
						Price = 10000000000,
						Brand = apple

					},
					new ProductModel
					{
						Name = "Dell vostro",
						Description = "PC is the best",
						Slug = "dellvostro",
						image = "./2.jpg",
						Category = pc,
						Price = 10000000000,
						Brand = samsung

					}
				]);  
			}
			_context.SaveChanges();
			
		}
	}
}
