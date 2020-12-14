using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Infrastructure.Specifications.Base;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Repositories.Base;
using StoreTemplateCore.Entities.Base;

namespace Infrastructure.Data.Repositories.Base
{
    
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected virtual DbContext Context { get; }
        private DbSet<T> EntitySet => Context.Set<T>();

        protected Repository(DbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException();
        }

        public async Task<T> AddAsync(T entity)
        {
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await EntitySet.Where(predicate).CountAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            EntitySet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await EntitySet.ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await EntitySet.Where(predicate).ToListAsync();
        }

        public async Task<T> GetSingleOrDefaultAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAsync(Expression<Func<T, bool>> predicate , Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = EntitySet;

            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }


        protected IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator.ApplySpecification(Context.Set<T>().AsQueryable(), spec);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await EntitySet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            //ToDo:Fix this problem
            T exist =  await Context.Set<T>().FindAsync(entity.Id);
            Context.Entry(exist).CurrentValues.SetValues(entity);
            await Context.SaveChangesAsync();
        }
    }
}
