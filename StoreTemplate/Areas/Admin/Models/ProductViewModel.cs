using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using StoreTemplateCore.Entities;

namespace StoreTemplate.Areas.Admin.Models
{
    public class ProductViewModel
    {
        [Required] 
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Summary { get; set; }

        public int UnitsInStock { get; set; }

        public IFormFile Image { get; set; }

        public static ProductViewModel InstanceByProduct(Product product)
        {
            return new ProductViewModel()
            {
                Id =  product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Summary = product.Summary,
                CategoryName = product.Category.Name,
                UnitsInStock = product.UnitsInStock ?? 0
            };
        }
    }
}
