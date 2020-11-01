using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Specifications.CategorySpecifications;
using Microsoft.EntityFrameworkCore;
using StoreTemplateCore.Entities;
using Infrastructure.Specifications;
using Infrastructure.Specifications.Base;
using ICategoryRepository = Infrastructure.Data.Repositories.Base.ICategoryRepository;

namespace Infrastructure.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(StoreDbContext context) : base(context)
        {

        }

        


        public async Task<IReadOnlyList<Product>> GetAllProductsByCategoryId(int id, int take = 0, int skip = 0)
        {
            var spec = new CategoryWithProductsSpecification(id);
            return (await GetAsync(spec)).FirstOrDefault()?.Products;
        }

        public async Task<IReadOnlyList<Product>> GetAllProductsByCategoryName(string name, int take = 0, int skip = 0)
        {
            var spec = new CategoryWithProductsSpecification(name);
            var category = (await GetAsync(spec)).First();

            return category.Products;
        }
    }
}
