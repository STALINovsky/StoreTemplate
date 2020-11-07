using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Specifications.CategorySpecifications;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Specifications.Base;


namespace StoreTemplate.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        private ICategoryRepository Repository { get; set; }

        public CategoryViewComponent(ICategoryRepository repository)
        {
            this.Repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var spec = new CategorySpecification().SortByName();
            return View(await Repository.GetAsync(spec));
        }
    }
}
