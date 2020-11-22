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
using Microsoft.EntityFrameworkCore;
using Infrastructure.Specifications.Base;
using Microsoft.AspNetCore.Identity;

namespace StoreTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository productRepository;
        private const int TopProductCount = 9;

        public HomeController(IProductRepository productRepository) : base()
        {
            var context = this.HttpContext;
            this.productRepository = productRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            var spec = new ProductSpecification().IncludeTags().SortByPopularity().AddPagination(TopProductCount);
            var products = await productRepository.GetAsync(spec);
            
            return View(products);
        }

        public IActionResult Contacts()
        {
            throw new NotImplementedException();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
