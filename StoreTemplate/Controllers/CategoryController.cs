using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data.Repositories.Base;
using Microsoft.AspNetCore.Mvc;

namespace StoreTemplate.Controllers
{
    public class CategoryController : Controller
    {
        private const int ProductsPerPage = 10;

        private ICategoryRepository Repository { get; set; }
        public CategoryController(ICategoryRepository repository)
        {
            Repository = repository;
        }

        [Route("{categoryName}/{page:int?}")]
        public async Task<IActionResult> Category(string categoryName, int page = 1)
        {
            //int skippedProductsCount = ProductsPerPage * (page - 1);
            //var category = new CategoryWithProductsSpecification(categoryName)
            //    .AddPagination(ProductsPerPage,skippedProductsCount);

            var categoryProducts = await Repository.GetAllProductsByCategoryName(categoryName, ProductsPerPage);

            return View(categoryProducts);
        }
    }
}
