using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Specifications.Base;
using StoreTemplateCore.Entities;

namespace Infrastructure.Data.Repositories.Base
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<IReadOnlyList<Product>> GetProductListAsync(ISpecification<Product> specification);
        public Task<IReadOnlyList<Product>> GetProductsByIds(IEnumerable<int> iDs);
        public Task<IReadOnlyList<Product>> GetProductsByNameAsync(string name);
        public Task<Product> GetProductByIdWithCategoryAsync(int id);
        public Task<IReadOnlyList<Product>> GetProductsByCategoryId(int categoryId);
    }
}
