using System.Collections.Generic;
using StoreTemplateCore.Entities.Base;

namespace StoreTemplateCore.Entities
{
    public class Category : Entity
    {
        public Category()
        {
        }
        public Category(string name, string description, string imageName)
        {
            Name = name;
            Description = description;
            ImageName = imageName;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public List<Product> Products { get; set; }

    }
}
