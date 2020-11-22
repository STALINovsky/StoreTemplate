using System.ComponentModel.DataAnnotations;
using StoreTemplateCore.Entities;

namespace Services.Model
{
    public class CartItem
    {
        public Product Product { get; set; }
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }

        public decimal Sum => Product.Price * Quantity;
    }
}
