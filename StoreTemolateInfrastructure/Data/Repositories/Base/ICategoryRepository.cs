using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Specifications.Base;
using StoreTemplateCore.Entities;

namespace Infrastructure.Data.Repositories.Base
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Task<IReadOnlyList<Product>> GetProductsOfCategoryAsync(ISpecification<Category> categorySpecification, ISpecification<Product> productsSpecification);
    }
}
