using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Specifications.Base.Extensions;
using Infrastructure.Specifications.CategorySpecifications;
using Infrastructure.Specifications.ProductSpecifications;
using Microsoft.AspNetCore.Mvc;

namespace StoreTemplate.Controllers
{
    public class CategoryController : Controller
    {
        private const int ProductsPerPage = 1;

        private ICategoryRepository Repository { get; set; }
        public CategoryController(ICategoryRepository repository)
        {
            Repository = repository;
        }

        [Route("[controller]/{categoryName}/{page:int?}")]
        public async Task<IActionResult> Category(string categoryName, int page = 1)
        {
            var categorySpecification = new CategorySpecification(categoryName);
            var productsSpecification = new ProductSpecification().AddPagination(ProductsPerPage);

            var categoryProducts = await Repository.GetProductsOfCategory(categorySpecification, productsSpecification);

            return View(categoryProducts);
        }
    }
}
