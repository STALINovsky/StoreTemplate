using System.ComponentModel.DataAnnotations;

namespace StoreTemplate.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Description")]
        public string Description { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter positive Price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please enter Category")]
        public string Category { get; set; }
    }
}