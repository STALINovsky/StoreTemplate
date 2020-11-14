using StoreTemplateCore.Entities;
using StoreTemplateCore.Entities.Base;

namespace StoreTemplateCore.Model
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal Sum { get => Product.Price * Quantity; }
    }
}
