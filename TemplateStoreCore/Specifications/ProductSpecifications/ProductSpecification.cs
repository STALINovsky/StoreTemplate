using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using StoreTemplateCore.Entities;
using StoreTemplateCore.Specifications.Base;

namespace StoreTemplateCore.Specifications.ProductSpecifications
{
    public class ProductSpecification : Specification<Product>
    {
        public ProductSpecification() : base()
        { }

        public ProductSpecification(Expression<Func<Product, bool>> expression) : base(expression)
        { }

    }
}
