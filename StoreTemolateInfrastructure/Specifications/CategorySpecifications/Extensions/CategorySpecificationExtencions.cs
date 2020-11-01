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
    }
}
