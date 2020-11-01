using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreTemplateCore.Entities;
using StoreTemplateCore.Repositories;
using StoreTemplateCore.Specifications.Base;
using StoreTemplateCore.Specifications.CategorySpecifications;
using StoreTemplateCore.Specifications.CategorySpecifications.Extensions;

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
            var spec = new CategorySpecification().AddSortingByName();
            return View( await Repository.GetAsync(spec));
        }
    }
}
