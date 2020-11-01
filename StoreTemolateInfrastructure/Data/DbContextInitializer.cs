using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StoreTemplateCore.Entities;
using StoreTemplateCore.Entities.Base;

namespace Infrastructure.Data
{
    public static class DbContextInitializer
    {
        public static async Task<bool> TryInitContext(DbContext context, ILogger logger)
        {
            try
            {
                await SeedCategoriesAsync(context);

                await SeedProductsAsync(context);
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return false;
            }

            return true;
        }

        private static async Task SeedAsync<T>(DbContext context, IEnumerable<T> entities) where T : Entity
        {
            var setOfEntity = context.Set<T>();
            if (await setOfEntity.AnyAsync())
                return;

            await setOfEntity.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        public static async Task SeedCategoriesAsync(DbContext context)
        {
            var categories = new Category[]
            {
                new Category() { Name = "Pc accessories",  }, // 1
                new Category() { Name = "Laptops", }, // 2
                new Category() {Name = "Smart" }, // 3 
                new Category() { Name = "Audio", }, // 4
                new Category() {Name = "Games" }, // 5
            };

            await SeedAsync(context, categories);
        }


        public static async Task SeedProductsAsync(DbContext context)
        {
            var products = new Product[]
            {
                new Product {
                    Name = "GeForce 4080ti",
                    CategoryId = 1,
                    Description = "Amazing GeForce",
                    Price = 999.99m,
                },
                new Product {
                    Name = "GeForce 5080ti",
                    CategoryId = 1,
                    Description = "Amazing GeForce",
                    Price = 1999.99m,
                },
                new Product {
                    Name = "Macbook pro",
                    CategoryId = 2,
                    Description = "Professional mac",
                    Price = 999.99m,
                },
                new Product {
                    Name = "Macbook pro 2",
                    CategoryId = 2,
                    Description = "Professional mac",
                    Price = 1999.99m,
                },
                new Product {
                    Name = "Iphone 10",
                    CategoryId = 3,
                    Description = "Amazing phone",
                    Price = 999.99m,
                },
                new Product {
                    Name = "Iphone 11",
                    CategoryId = 3,
                    Description = "Amazing phone",
                    Price = 1999.99m,
                },
                new Product {
                    Name = "AirPods",
                    CategoryId = 4,
                    Description = "Amazing Audio",
                    Price = 999.99m,
                },
                new Product {
                    Name = "AirPods 2",
                    CategoryId = 4,
                    Description = "Amazing Audio",
                    Price = 1999.99m,
                },
                new Product {
                    Name = "Witcher",
                    CategoryId = 5,
                    Description = "good game",
                    Price = 999.99m,
                },
                new Product {
                    Name = "Witcher 3",
                    CategoryId = 5,
                    Description = "Amazing game",
                    Price = 1999.99m,
                },
            };

            await SeedAsync(context, products);
        }

    }
}
