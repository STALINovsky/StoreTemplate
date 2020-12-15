using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreTemplateCore.Entities;
using StoreTemplateCore.Identity;
using System.Linq;
using StoreTemplateCore.Entities.Base;

namespace Infrastructure.Data.Contexts
{
    public sealed class StoreDbContext : IdentityDbContext<User>
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>().Property(prod => prod.Name).IsRequired();
            builder.Entity<Product>().HasIndex(prod => prod.Name).IsUnique(true);

            builder.Entity<Category>().Property(category => category.Name).IsRequired();
            builder.Entity<Category>().HasIndex(category => category.Name).IsUnique(true);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderDetails> OrdersDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
