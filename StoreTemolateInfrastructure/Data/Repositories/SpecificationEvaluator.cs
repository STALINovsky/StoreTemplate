﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreTemplateCore.Entities.Base;
using StoreTemplateCore.Specifications.Base;

namespace Infrastructure.Data.Repositories
{
    internal static class SpecificationEvaluator<T> where T : Entity
	{
		public static IQueryable<T> GetQuery(IQueryable<T> baseQuery, ISpecification<T> specification)
		{
			var query = baseQuery;

			//filter by Criteria
			if (specification.Criteria != null) query = query.Where(specification.Criteria);
			
            //
			specification.Includes?.Aggregate(query,
				(current, include) => current.Include(include));

			specification.IncludeStrings?.Aggregate(query,
				(current, include) => current.Include(include));

			// add Ordering
            query = specification.OrderByExpressions.Aggregate(query, 
                (current, expression) => current.OrderBy(expression));

            query = specification.OrderByDescendingExpressions.Aggregate(query,
                (current, expression) => current.OrderByDescending(expression));

			//Adding pagination
            if (specification.Skip > 0)
				query = query.Skip(specification.Skip);

			if (specification.Take > 0)
				query = query.Take(specification.Take);


			return query;
		}
	}
}