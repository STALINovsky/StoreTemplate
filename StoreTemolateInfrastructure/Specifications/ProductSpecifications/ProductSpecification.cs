using System;
using System.Linq.Expressions;
using Infrastructure.Specifications.Base;
using StoreTemplateCore.Entities;

namespace Infrastructure.Specifications.ProductSpecifications
{
    public class ProductSpecification : Specification<Product>
    {
        public ProductSpecification() : base()
        { }

        public ProductSpecification(Expression<Func<Product, bool>> expression) : base(expression)
        { }

    }
}
