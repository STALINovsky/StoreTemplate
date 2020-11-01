using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreTemplateCore.Repositories;

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
        public IActionResult Category(string categoryName, int page = 1)
        {
            
            return View();
        }
    }
}
