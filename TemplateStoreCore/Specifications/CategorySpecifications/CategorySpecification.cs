using System;
using System.Linq.Expressions;
using StoreTemplateCore.Entities;
using StoreTemplateCore.Specifications.Base;

namespace StoreTemplateCore.Specifications.CategorySpecifications
{
    public class CategorySpecification : Specification<Category>
    {
        public CategorySpecification() : base() { }
        public CategorySpecification(Expression<Func<Category,bool>> expression) : base(expression) { }
    }
}