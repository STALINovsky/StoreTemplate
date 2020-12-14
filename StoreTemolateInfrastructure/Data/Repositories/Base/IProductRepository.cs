using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Specifications.Base;
using StoreTemplateCore.Entities;

namespace Infrastructure.Data.Repositories.Base
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetProductListAsync(ISpecification<Product> specification);
        Task<IReadOnlyList<Product>> GetProductsByIds(IEnumerable<int> iDs);
        Task<IReadOnlyList<Product>> FindProductsByName(string name);
        Task<Product> GetProductByIdWithCategoryAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsByCategoryId(int categoryId);
        Task<Product> GetProductByNameOrDefault(string name);
        Task<IReadOnlyCollection<Product>> GetProductsByNames(IEnumerable<string> names);
    }
}
