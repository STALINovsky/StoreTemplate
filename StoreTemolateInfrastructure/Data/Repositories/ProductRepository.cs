using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Specifications.Base;
using Infrastructure.Specifications.ProductSpecifications;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Specifications;
using StoreTemplateCore.Entities;

namespace Infrastructure.Data.Repositories
{
    public sealed class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(StoreDbContext context) : base(context)
        {

        }

        public async Task<Product> GetProductByIdWithCategoryAsync(int id)
        {
            var spec = new ProductSpecification(id).IncludeCategory();
            return (await GetAsync(spec)).FirstOrDefault();
        }

        public async Task<IReadOnlyList<Product>> GetProductsByNameAsync(string name)
        {
            var spec = new ProductSpecification(name);
            return await GetAsync(spec);
        }

        public async Task<IReadOnlyList<Product>> GetProductListAsync(ISpecification<Product> specification)
        {
            return await GetAsync(specification);
        }

        public async Task<IReadOnlyList<Product>> GetProductsByCategoryId(int categoryId)
        {
            var spec = new ProductSpecification(product => product.CategoryId == categoryId);

            return (await GetAsync(spec));
        }
    }
}
