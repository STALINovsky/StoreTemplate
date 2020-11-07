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

        int Take { get; set; }
        int Skip { get; set; }

    }
}
