using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Constants;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Specifications.NewsRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Services.Services.Interfaces;
using StoreTemplate.Areas.Admin.Models;
using StoreTemplateCore.Entities;

namespace StoreTemplate.Areas.Admin.Controllers
{
    [Area(AreaNamesConstants.AdminAreaName)]
    [Authorize(Roles = IdentityRoleConstants.AdminRoleName + "," + IdentityRoleConstants.ManagerRoleName)]
    public class NewsController : Controller
    {
        private readonly INewsRepository newsRepository;
        private readonly IImageService imageService;

        public NewsController(INewsRepository newsRepository, IImageService imageService)
        {
            this.newsRepository = newsRepository;
            this.imageService = imageService;
        }

        public async Task<IActionResult> List()
        {
            var newsSpecification = new NewsSpecification().OrderByDate();
            var news = await newsRepository.GetAsync(newsSpecification);

            return View(news);
        }

        public async Task<IActionResult> Index(int id)
        {
            var newsSpec = new NewsSpecification(id);
            var news = await newsRepository.GetSingleOrDefaultAsync(newsSpec);
            var newsViewModel = NewsViewModel.CreateByNews(news);
            return View(newsViewModel);
        }

        private async Task<News> InitNewsByViewModel(NewsViewModel viewModel)
        {
            var news = new News()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Text = viewModel.Text,
            };
            if (viewModel.Image != null)
            {
                news.ImagePath = await imageService.SaveImage(typeof(News), news.Name, viewModel.Image);
            }

            return news;
        }

        [HttpGet]
        public IActionResult AddNews()
        {
            return View(new NewsViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddNews(NewsViewModel newsViewModel)
        {
            var news = await InitNewsByViewModel(newsViewModel);

            await newsRepository.AddAsync(news);

            return RedirectToAction(nameof(List));
        }


        [HttpPost]
        public async Task<IActionResult> Update(NewsViewModel viewModel)
        {
            var news = await InitNewsByViewModel(viewModel);

            await newsRepository.UpdateAsync(news);

            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(NewsViewModel viewModel)
        {
            var news = await InitNewsByViewModel(viewModel);
            await newsRepository.DeleteAsync(news);
            return RedirectToAction(nameof(List));
        }

    }
}
