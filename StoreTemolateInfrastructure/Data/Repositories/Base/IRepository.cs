using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Infrastructure.Specifications.Base;
using StoreTemplateCore.Entities.Base;

namespace Infrastructure.Data.Repositories.Base
{
    public interface IRepository<T> where T : Entity
    {
        Task<IReadOnlyCollection<T>> GetAllAsync();

        Task<IReadOnlyCollection<T>> GetAsync(Expression<Func<T,bool>> predicate);
        Task<IReadOnlyCollection<T>> GetAsync(Expression<Func<T,bool>> predicate = null, Func<IQueryable<T>,IOrderedQueryable<T>> orderBy = null);
        public Task<IReadOnlyList<T>> GetAsync(ISpecification<T> specification);
        public Task<T> GetSingleOrDefaultAsync(ISpecification<T> specification);

        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task<T> GetByIdAsync(int id);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}
