using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Specifications.CategorySpecifications;
using Infrastructure.Specifications.NewsRepository;
using Microsoft.AspNetCore.Mvc;

namespace StoreTemplate.Components
{
    public class NewsViewComponent : ViewComponent
    {
        private readonly INewsRepository newsRepository;
        public NewsViewComponent(INewsRepository newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var spec = new NewsSpecification().OrderByDate().AddPagination(3);
            var news = await newsRepository.GetAsync(spec);
            return View(news);
        }
    }
}
