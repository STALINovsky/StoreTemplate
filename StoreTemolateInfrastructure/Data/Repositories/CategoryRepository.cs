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
using Microsoft.EntityFrameworkCore.Internal;

namespace Infrastructure.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(StoreDbContext context) : base(context)
        {
            Categories = context.Categories;

        }

        private DbSet<Category> Categories { get; set; }

        public async Task<IReadOnlyList<Product>> GetAllProductsByCategoryId(int id, int take = 0, int skip = 0)
        {
            var spec = new CategorySpecification(id);
            return (await GetAsync(spec)).FirstOrDefault()?.Products;
        }

        public async Task<IReadOnlyList<Product>> GetProductsOfCategory
            (ISpecification<Category> categorySpecification, ISpecification<Product> productsSpecification)
        {
            var category = await ApplySpecification(categorySpecification).FirstAsync();

            var productsCategoryQuery = Context.Entry(category).Collection(cat => cat.Products).Query();
            var productsOfCategory = await SpecificationEvaluator<Product>.ApplySpecification
                (productsCategoryQuery, productsSpecification).ToListAsync();
            return productsOfCategory;
        }
    }
}
