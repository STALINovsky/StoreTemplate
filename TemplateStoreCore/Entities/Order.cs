using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using StoreTemplateCore.Entities.Base;
using StoreTemplateCore.Identity;

namespace StoreTemplateCore.Entities
{
    public class Order : Entity
    {
        public User User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public DateTime CreateTime { get; set; }
        public OrderDetails OrderDetails { get; set; }
        public bool IsActive { get; set; }

        [NotMapped] 
        public decimal Sum => OrderItems.Sum(item => item.Product.Price);
    }
}   
