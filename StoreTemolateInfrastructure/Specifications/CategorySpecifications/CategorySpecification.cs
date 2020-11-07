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

        public CategorySpecification(string name):base(category => category.Name.ToLower().Contains(name.ToLower())) { }
        public CategorySpecification(int id) : base(category => category.Id == id){ }

        public ISpecification<Category> SortByName()
        {
            AddOrdering(category => category.Name);
            return this;
        }

    }
}