using System;


namespace Infrastructure.Specifications.Base.Extensions
{
    public static class SpecificationExtensions
    {
        public static ISpecification<T> AddPagination<T>(this ISpecification<T> specification, int take = 0, int skip = 0)
        {
            specification.Take = take;
            specification.Skip = skip;

            return specification;
        }
    }
}
