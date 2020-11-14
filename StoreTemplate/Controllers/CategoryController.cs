using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Specifications;
using Infrastructure.Specifications.CategorySpecifications;
using Infrastructure.Specifications.ProductSpecifications;
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
        [Route("[controller]/{categoryName}/{page?}")]
        public async Task<IActionResult> Index(string categoryName, int page = 1)
        {
            var categorySpecification = new CategorySpecification(categoryName);
            var productsSpecification = new ProductSpecification().SortByPopularity()
                .AddPagination(ProductsPerPage, page);

            var categoryProducts = await Repository.GetProductsOfCategoryAsync(categorySpecification, productsSpecification);

            return View(categoryProducts);
        }
    }
}
