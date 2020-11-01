using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure.Specifications.Base
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }

        List<Expression<Func<T, object>>> OrderByExpressions { get; }
        List<Expression<Func<T, object>>> OrderByDescendingExpressions { get; }

        public int Take { get; set; }
        public int Skip { get; set; }

        public void AddOrdering(Expression<Func<T, object>> expression);
        public void AddDescendingOrdering(Expression<Func<T, object>> expression);
        public void AddInclude(Expression<Func<T, object>> includeExpression);
        public void AddInclude(string includeString);

    }
}
