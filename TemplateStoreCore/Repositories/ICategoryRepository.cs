using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreTemplateCore.Entities;

namespace StoreTemplateCore.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Task<IReadOnlyList<Product>> getAllProductsByCategoryId(int id, int take = 0, int skip = 0);

        public Task<IReadOnlyList<Product>> getAllProductsByCategoryName(string name, int take, int skip = 0);

    }
}
