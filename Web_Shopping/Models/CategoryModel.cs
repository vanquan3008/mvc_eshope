using System.ComponentModel.DataAnnotations;

namespace Web_Shopping.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id_Category {  get;set; }
        [Required, MinLength(4, ErrorMessage = "Require user input name Category")]
        public string Name { get;set;  }
        public string Slug { get; set; }
        [Required, MinLength(4, ErrorMessage = "Require input description Brand")]
        public string Description {  get;set; }
        public int Status {  get; set; }
    }
}