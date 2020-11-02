using System.Runtime.CompilerServices;
using Infrastructure.Specifications.Base;
using StoreTemplateCore.Entities;

namespace Infrastructure.Specifications.CategorySpecifications.Extensions
{
    public static class CategorySpecificationExtensions
    {
        public static ISpecification<Category> AddSortingByName(this ISpecification<Category> specification)
        {
            specification.AddOrdering(category => category.Name);
            return specification;
        }

        public static ISpecification<Category> IncludeProducts(this ISpecification<Category> specification)
        {
            specification.AddInclude(spec => spec.Products);
            return specification;
        }
    }
}
