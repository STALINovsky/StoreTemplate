using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Specifications.Base;
using StoreTemplateCore.Entities;

namespace Infrastructure.Data.Repositories.Base
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Task<IReadOnlyList<Product>> GetAllProductsByCategoryId(int id, int take = 0, int skip = 0);

        public Task<IReadOnlyList<Product>> GetProductsOfCategory(ISpecification<Category> categorySpecification, ISpecification<Product> productsSpecification);
    }
}
