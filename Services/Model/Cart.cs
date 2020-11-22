using System.Collections.Generic;
using System.Linq;
using StoreTemplateCore.Entities;

namespace Services.Model
{
    public class Cart
    {
        public IReadOnlyList<CartItem> Items { get; }
        public decimal TotalSum => Items.Sum(item => item.Sum);

        public Cart(IReadOnlyList<CartItem> items)
        {
            Items = items;
        }

    }
}

