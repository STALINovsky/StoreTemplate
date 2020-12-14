using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using StoreTemplateCore.Entities.Base;

namespace StoreTemplateCore.Entities
{
    public class Category : Entity
    {
        public Category()
        {
        }

        public Category(string name, string description, string imagePath)
        {
            Name = name;
            Description = description;
            ImagePath = imagePath;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        [Column("ImageName")]
        public string ImagePath { get; set; }
        public List<Product> Products { get; set; }

    }
}
