﻿using System;
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
using StoreTemplateCore.Identity;

namespace StoreTemplate.Controllers
{
    public class HomeController : Controller
    {
        readonly ILogger<HomeController> Logger;
        readonly IProductRepository ProductRepository;
        const int TopProductCount = 3;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            this.ProductRepository = productRepository;
            Logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var spec = new ProductSpecification().IncludeTags().SortByPopularity().AddPagination(TopProductCount);
            var products = await ProductRepository.GetAsync(spec);
            
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
