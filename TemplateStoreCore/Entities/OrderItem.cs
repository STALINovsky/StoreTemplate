using System;
using System.Collections.Generic;
using System.Text;
using StoreTemplateCore.Entities.Base;

namespace StoreTemplateCore.Entities
{
    public class OrderItem : Entity
    {
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
