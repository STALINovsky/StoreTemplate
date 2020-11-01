using Infrastructure.Specifications.Base;
using StoreTemplateCore.Entities;

namespace Infrastructure.Specifications.ProductSpecifications.Extensions
{
    public static class ProductSpecificationExtensions
    {
        public static ISpecification<Product> AddSortingByStars(this ISpecification<Product> specification)
        {
            specification.AddOrdering(product => product.Stars ?? 0);
            return specification;
        }

        public static ISpecification<Product> SortByName(this ISpecification<Product> specification)
        {
            specification.AddDescendingOrdering(product => product.Name);
            return specification;
        }
    }
}
