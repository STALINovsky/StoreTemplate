using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using StoreTemplateCore.Entities;
using StoreTemplateCore.Repositories;
using StoreTemplateCore.Specifications;
using StoreTemplateCore.Specifications.Base;
using StoreTemplateCore.Specifications.ProductSpecifications;

namespace Infrastructure.Data.Repositories
{
    public sealed class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(StoreDbContext context) : base(context)
        {

        }

        private new StoreDbContext Context => (StoreDbContext)base.Context;

        public async Task<Product> GetProductByIdWithCategoryAsync(int id)
        {
            var spec = new ProductWithCategorySpecification(id);
            return (await GetAsync(spec)).FirstOrDefault();
        }

        public async Task<IReadOnlyList<Product>> GetProductByNameAsync(string name)
        {
            var spec = new ProductWithCategorySpecification(name);
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
