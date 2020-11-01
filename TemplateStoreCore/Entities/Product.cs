﻿using System.ComponentModel.DataAnnotations;
using StoreTemplateCore.Entities.Base;

namespace StoreTemplateCore.Entities
{
    public class Product : Entity
    {
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFileName { get; set; }
        public decimal Price { get; set; }
        public int? UnitsInStock { get; set; }

        public double? Stars { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

    }
}