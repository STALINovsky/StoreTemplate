using System;
using System.Linq.Expressions;
using Infrastructure.Specifications.Base;
using StoreTemplateCore.Entities;

namespace Infrastructure.Specifications.CategorySpecifications
{
    public class CategorySpecification : Specification<Category>
    {
        public CategorySpecification() : base() { }
        public CategorySpecification(Expression<Func<Category,bool>> expression) : base(expression) { }
    }
}