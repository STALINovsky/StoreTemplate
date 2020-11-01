using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace StoreTemplateCore.Specifications.Base
{
    public abstract class Specification<T> : ISpecification<T>
    {
        protected Specification()
        {

        }

        protected Specification(Expression<Func<T, bool>> criteria)
        {
            this.Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();

        public List<Expression<Func<T, object>>> OrderByExpressions { get; private set; } = new List<Expression<Func<T, object>>>();
        public List<Expression<Func<T, object>>> OrderByDescendingExpressions { get; private set; } = new List<Expression<Func<T, object>>>();

        public int Skip { get; set; }
        public int Take { get; set; }

        public void AddOrdering(Expression<Func<T, object>> expression)
        {
            OrderByExpressions.Add(expression);
        }

        public void AddDescendingOrdering(Expression<Func<T, object>> expression)
        {
            OrderByDescendingExpressions.Add(expression);
        }

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }
}
