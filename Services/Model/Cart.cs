using System.Collections.Generic;
using System.Linq;
using StoreTemplateCore.Entities;
using StoreTemplateCore.Model;

namespace Services.Model
{
    public class Cart
    {
        public string UserName { get; set; }
        private List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal Sum { get => Items.Sum(item => item.Sum); }

        public virtual decimal CalculateTotalValue()
        {
            decimal sum = 0;
            foreach (var line in Items)
            {
                sum += line.Product.Price * line.Quantity;
            }
            return sum;
        }

        public virtual void AddNewProduct(Product product, int quantity)
        {
            var entryLine = Items.FirstOrDefault(line => line.Product.Id == product.Id);

            if (entryLine == null)
            {
                Items.Add(new CartItem() { Product = product, Quantity = quantity });
            }
            else
            {
                entryLine.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(int productId)
        {
            var entryLine = Items.FirstOrDefault(line => line.Product.Id == productId);
            Items.Remove(entryLine);
        }

        public virtual void Clear()
        {
            Items.Clear();
        }
    }
}
