using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using StoreTemplateCore.Entities;
using StoreTemplateCore.Repositories;
using StoreTemplateCore.Specifications;
using StoreTemplateCore.Specifications.Base;
using StoreTemplateCore.Specifications.CategorySpecifications;

namespace Infrastructure.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(StoreDbContext context) : base(context)
        {

        }

        


        public async Task<IReadOnlyList<Product>> getAllProductsByCategoryId(int id, int take = 0, int skip = 0)
        {
            var spec = new CategoryWithProductSpecification(id);
            return (await GetAsync(spec)).FirstOrDefault()?.Products;
        }

        public async Task<IReadOnlyList<Product>> getAllProductsByCategoryName(string name, int take = 0, int skip = 0)
        {
            var spec = new CategoryWithProductSpecification(name);
            return (await GetAsync(spec)).FirstOrDefault()?.Products;
        }
    }
}
