using StoreTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreTemplate.Models.Data
{
    public interface IProductRepository
    {
        public IQueryable<Product> Products { get; }
        public void SaveProduct(Product product);
        public Product DeleteProduct(int productId);
    }
}