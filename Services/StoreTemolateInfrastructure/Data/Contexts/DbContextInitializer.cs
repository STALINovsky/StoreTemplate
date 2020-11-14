using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StoreTemplate.Constants;
using StoreTemplateCore.Entities;
using StoreTemplateCore.Entities.Base;
using StoreTemplateCore.Model.Identity;


namespace Infrastructure.Data.Contexts
{
    public static class DbContextInitializer
    {
        public static async Task<bool> TryInitEntities(DbContext context, ILogger logger)
        {
            try
            {
                await SeedTagsAsync(context);
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

        private static async Task SeedCategoriesAsync(DbContext context)
        {
            var categories = new Category[]
            {
                new Category() { Name = "Pc accessories",  }, // 1
                new Category() { Name = "Laptops", }, // 2
                new Category() { Name = "Smart" }, // 3 
                new Category() { Name = "Audio", }, // 4
                new Category() { Name = "Games" }, // 5
            };

            await SeedAsync(context, categories);
        }

        private static async Task SeedTagsAsync(DbContext context)
        {
            var tags = new Tag[]
            {
                new Tag() { Name = "Electronics"}, // 1
                new Tag() { Name = "HiTech" }, // 2
                new Tag() { Name = "Phone" },// 3
                new Tag() { Name = "Gaming" },// 4
                new Tag() { Name = "Users choice" }, // 5
            };
            await SeedAsync(context, tags);
        }

        private static async Task SeedProductsAsync(DbContext context)
        {
            var products = new Product[]
            {
                new Product {
                    Name = "GeForce 4080ti",
                    CategoryId = 1,
                    Description = "Amazing GeForce",
                    ImagePath = @"Images/Product/Gtx.jpg",
                    Price = 999.99m,
                    Tags = new List<Tag>()
                    {
                        new Tag() { Name = "Electronics" },
                        new Tag() { Name = "Gaming" },
                    },
                },
                new Product {
                    Name = "GeForce 5080ti",
                    CategoryId = 1,
                    Description = "Amazing GeForce",
                    ImagePath = @"Images/Product/Gtx.jpg",
                    Price = 1999.99m,
                    Tags = new List<Tag>()
                    {
                        new Tag() { Name = "Gaming" },
                        new Tag() { Name = "Electronics" },
                    },
                },
                new Product {
                    Name = "Macbook pro",
                    CategoryId = 2,
                    Description = "Professional mac",
                    ImagePath = @"Images/Product/Mac-book.png",
                    Price = 999.99m,
                    Tags = new List<Tag>()
                    {
                        new Tag() { Name = "HiTech" },
                        new Tag() { Name = "Users choice" },
                    },
                },
                new Product {
                    Name = "Macbook pro 2",
                    CategoryId = 2,
                    Description = "Professional mac",
                    Price = 1999.99m,
                    ImagePath = @"Images/Product/Mac-book.png",
                    Tags = new List<Tag>()
                    {
                        new Tag() { Name = "HiTech" },
                        new Tag() { Name = "Electronics" },
                    },
                },
                new Product {
                    Name = "Iphone 10",
                    CategoryId = 3,
                    Description = "Amazing phone",
                    Price = 999.99m,
                    ImagePath = @"Images/Product/Iphone.jpg",
                    Tags = new List<Tag>()
                    {
                        new Tag() { Name = "Phone" },
                        new Tag() { Name = "Electronics" },
                    },
                },
                new Product {
                    Name = "Iphone 11",
                    CategoryId = 3,
                    Description = "Amazing phone",
                    Price = 1999.99m,
                    ImagePath = @"Images/Product/Iphone.jpg",
                    Tags = new List<Tag>()
                    {
                        new Tag() { Name = "Phone" },
                        new Tag() { Name = "Users choice" },
                    },
                },
                new Product {
                    Name = "AirPods",
                    CategoryId = 4,
                    Description = "Amazing Audio",
                    Price = 999.99m,
                    ImagePath = @"Images/Product/AirPods.jpg",
                    Tags = new List<Tag>()
                    {
                        new Tag() { Name = "Electronics" },
                        new Tag() { Name = "Users choice" },
                    },
                },
                new Product {
                    Name = "AirPods 2",
                    CategoryId = 4,
                    Description = "Amazing Audio",
                    Price = 1999.99m,
                    ImagePath = @"Images/Product/AirPods.jpg",
                    Tags = new List<Tag>()
                    {
                        new Tag() { Name = "HiTech" },
                        new Tag() { Name = "Users choice" },
                    },
                },
                new Product {
                    Name = "Witcher",
                    CategoryId = 5,
                    Description = "good game",
                    Price = 999.99m,
                    ImagePath = @"Images/Product/Witcher.jpg",
                    Tags = new List<Tag>()
                    {
                        new Tag() { Name = "Gaming" },
                    },
                },
                new Product {
                    Name = "Witcher 3",
                    CategoryId = 5,
                    Description = "Amazing game",
                    Price = 1999.99m,
                    ImagePath = @"Images/Product/Witcher.jpg",
                    Tags = new List<Tag>()
                    {
                        new Tag() { Name = "Gaming" },
                        new Tag() { Name = "Users choice"}
                    },
                },
            };

            await SeedAsync(context, products);
        }

        public static async Task InitIdentity
            (UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            await EnsureRoleCreating(IdentityRoleConstants.AdminRoleName, roleManager);
            await EnsureRoleCreating(IdentityRoleConstants.ManagerRoleName, roleManager);
            await EnsureRoleCreating(IdentityRoleConstants.VisitorRoleName, roleManager);

            var adminAccountSection = configuration.GetSection("AdminAccount");
            var adminEmail = adminAccountSection["AdminEmail"];
            var adminPassword = adminAccountSection["AdminPassword"];

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                var admin = new User() {Email = adminEmail, UserName = "Admin"};
                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                    await userManager.AddToRoleAsync(admin, "Manager");
                }
            }
        }

        private static async Task EnsureRoleCreating(string roleName, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
