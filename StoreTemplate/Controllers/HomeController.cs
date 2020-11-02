using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data.Repositories;
using Infrastructure.Data;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Specifications.ProductSpecifications;
using Infrastructure.Specifications.ProductSpecifications.Extensions;
using Microsoft.EntityFrameworkCore;
using StoreTemplateCore.Entities;
using Infrastructure.Specifications.Base;
using Infrastructure.Specifications.Base.Extensions;

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
            var spec = new ProductSpecification().IncludeTags().AddSortingByStars().AddPagination(TopProductCount);
            var products = await ProductRepository.GetAsync(spec);
            
            return View(products);
        }


        public IActionResult Privacy()
        {
            return View();
        }
    }
}
