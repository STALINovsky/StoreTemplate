using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Specifications.CategorySpecifications;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Specifications;
using Infrastructure.Specifications.Base;
using Microsoft.EntityFrameworkCore.Internal;
using StoreTemplateCore.Entities;

namespace Infrastructure.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(StoreDbContext context) : base(context)
        {
            Categories = context.Categories;
        }

        private DbSet<Category> Categories { get; set; }

        public async Task<IReadOnlyList<Product>> GetProductsOfCategoryAsync
            (ISpecification<Category> categorySpecification, ISpecification<Product> productsSpecification)
        {
            var category = await ApplySpecification(categorySpecification).FirstAsync();

            var productsQuery = Context.Entry(category).Collection(cat => cat.Products).Query();
            
            var productsOfCategory = await SpecificationEvaluator.ApplySpecification
                (productsQuery, productsSpecification).ToListAsync();

            return productsOfCategory;
        }
    }
}
