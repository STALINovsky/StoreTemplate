using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StoreTemplateCore.Entities.Base;

namespace StoreTemplateCore.Entities
{
    public class Product : Entity
    {
        [Required(ErrorMessage = "Please Enter Product")]
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public int? UnitsInStock { get; set; }

        public double Stars { get; set; }

        // n - 1 relationship
        public int? CategoryId { get; set; }
        public Category Category { get; set; }


        // 1 - n relationship
        public List<Tag> Tags { get; set; }

        public const int MaxStarsCount = 5;
    }
}