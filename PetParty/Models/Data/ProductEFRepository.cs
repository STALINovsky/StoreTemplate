using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreTemplate.Models.Data
{
    public class ProductEFRepository : IProductRepository
    {
        public ProductEFRepository(StoreDbContext context)
        {
            this.DbContext = context;
        }

        private StoreDbContext DbContext { get; set; }

        public IQueryable<Product> Products => throw new NotImplementedException();

        public Product DeleteProduct(int productId)
        {
            Product productToDelete = Products.FirstOrDefault(product => product.Id == productId);
            if (productToDelete != null)
            {
                DbContext.Products.Remove(productToDelete);
                DbContext.SaveChanges();
            }

            return productToDelete;
        }

        public void SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                DbContext.Add(product);
            }
            else
            {
                DbContext.Attach(product);
            }

            DbContext.SaveChanges();
        }
    }
}
