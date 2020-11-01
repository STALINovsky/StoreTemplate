using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data.Repositories;
using Infrastructure.Data;
using StoreTemplateCore.Entities;
using StoreTemplateCore.Repositories;
using StoreTemplateCore.Specifications.Base;
using StoreTemplateCore.Specifications.Base.Extensions;
using StoreTemplateCore.Specifications.ProductSpecifications;
using StoreTemplateCore.Specifications.ProductSpecifications.Extensions;

namespace StoreTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> Logger;
        private readonly IProductRepository ProductRepository;
        private const int TopProductCount = 3;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            this.ProductRepository = productRepository;
            Logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var spec = new ProductSpecification().AddSortingByStars().AddPagination(TopProductCount);
            var topOfProducts = await ProductRepository.GetAsync(spec);

            return View(topOfProducts);
        }


        public IActionResult Privacy()
        {
            return View();
        }
    }
}
