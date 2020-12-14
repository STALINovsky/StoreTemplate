using System.Collections.Generic;
using StoreTemplateCore.Entities.Base;

namespace StoreTemplateCore.Entities
{
    public class Tag : Entity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
