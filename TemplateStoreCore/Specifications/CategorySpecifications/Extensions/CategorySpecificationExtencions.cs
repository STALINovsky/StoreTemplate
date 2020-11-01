using System;
using System.Collections.Generic;
using System.Text;
using StoreTemplateCore.Entities;
using StoreTemplateCore.Specifications.Base;

namespace StoreTemplateCore.Specifications.CategorySpecifications.Extensions
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
