using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StoreTemplate.Models.Data
{
    public sealed class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }
    }
}
