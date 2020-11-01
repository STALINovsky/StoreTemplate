using System.Collections.Generic;
using System.Threading.Tasks;
using StoreTemplateCore.Entities;
using StoreTemplateCore.Specifications.Base;

namespace StoreTemplateCore.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<IReadOnlyList<Product>> GetProductListAsync(ISpecification<Product> specification);
        public Task<IReadOnlyList<Product>> GetProductByNameAsync(string name);
        public Task<Product> GetProductByIdWithCategoryAsync(int id);
        public Task<IReadOnlyList<Product>> GetProductsByCategoryId(int categoryId);
    }
}
