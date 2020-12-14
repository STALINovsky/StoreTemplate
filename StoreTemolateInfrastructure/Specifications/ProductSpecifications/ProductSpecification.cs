using System;
using System.Linq.Expressions;
using Infrastructure.Specifications.Base;
using StoreTemplateCore.Entities;

namespace Infrastructure.Specifications.ProductSpecifications
{
    public class ProductSpecification : Specification<Product>
    {
        public ProductSpecification() : base() { }

        public ProductSpecification(int id) :this(product => product.Id == id) { }

        public ProductSpecification(string name) : this(product => product.Name.ToLower() == (name.ToLower())) { }

        public ProductSpecification(Expression<Func<Product, bool>> expression) : base(expression) { }


        public ProductSpecification SortByPopularity()
        {
            AddOrdering(product => product.Stars);
            return this;
        }

        public ProductSpecification SortByName()
        {
            AddDescendingOrdering(product => product.Name);
            return this;
        }

        public ProductSpecification IncludeCategory()
        {
            AddInclude(product => product.Category);
            return this;
        }

        public ProductSpecification IncludeTags()
        {
            AddInclude(product => product.Tags);
            return this;
        }
    }
}
