using Microsoft.EntityFrameworkCore;
using StoreTemplateCore.Entities;

namespace Infrastructure.Data
{
    public sealed class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
