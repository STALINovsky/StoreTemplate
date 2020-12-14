using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Specifications.ProductSpecifications;

namespace StoreTemplate.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository repository;

        public ProductsController(IProductRepository repository)
        {
            this.repository = repository;
        }

        [Route("[controller]/{productName}")]
        public async Task<IActionResult> Index(string productName)
        {
            var product = await repository.GetProductByNameOrDefault(productName);
            return View(product);
        }
    }
}
