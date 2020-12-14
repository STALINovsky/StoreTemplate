using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Specifications.Base;
using Infrastructure.Specifications.CategorySpecifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Services.Interfaces;
using StoreTemplate.Areas.Admin.Models;
using StoreTemplate.Areas.Constants;
using StoreTemplateCore.Entities;

namespace StoreTemplate.Areas.Admin.Controllers
{
    [Area(AreaNamesConstants.AdminAreaName)]
    public class CategoriesController : Controller
    {
        private const int CategoriesCountPerPage = 10;
        private readonly ICategoryRepository categoryRepository;
        private readonly IImageService imageService;

        public CategoriesController(ICategoryRepository categoryRepository, IImageService imageService)
        {
            this.categoryRepository = categoryRepository;
            this.imageService = imageService;
        }

        public async Task<IActionResult> Index(string name)
        {
            var categorySpecification = new CategorySpecification(name);
            var category = await categoryRepository.GetSingleOrDefaultAsync(categorySpecification);

            return View(CategoryViewModel.InstanceByCategory(category));
        }

        public async Task<IActionResult> List(int page = 1)
        {
            var categorySpecification = new CategorySpecification().AddPagination(CategoriesCountPerPage, page);
            var categoryList = await categoryRepository.GetAsync(categorySpecification);
            return View(categoryList);
        }

        private async Task<Category> UpdateCategoryByViewModel(Category category, CategoryViewModel viewModel)
        {

            category.Id = viewModel.Id;
            category.Name = viewModel.Name;
            category.Description = viewModel.Description;

            if (viewModel.Image != null)
            {
                category.ImagePath = await imageService.SaveImage(category.GetType(), viewModel.Name, viewModel.Image);
            }

            return category;
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryViewModel viewModel)
        {
            var oldCategory = await categoryRepository.GetSingleOrDefaultAsync(new CategorySpecification(viewModel.Id));

            var category = await UpdateCategoryByViewModel(oldCategory, viewModel);

            try
            {
                await categoryRepository.UpdateAsync(category);
            }
            catch (DbUpdateException exception)
            {
                ModelState.AddModelError(nameof(viewModel.Name), "Name needs to be unique!");
                return View(nameof(Index), viewModel);
            }
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel viewModel)
        {
            var category = await UpdateCategoryByViewModel(new Category(), viewModel);

            try
            {
                await categoryRepository.AddAsync(category);
            }
            catch (DbUpdateException exception)
            {
                ModelState.AddModelError(nameof(category.Name), "Name needs to be unique");
                if (viewModel.Image != null)
                {
                    imageService.DeleteImage(category.GetType(), viewModel.Name, viewModel.Image);
                }
                return View(nameof(Index), viewModel);
            }

            return RedirectToAction("List");
        }

        public async Task<IActionResult> Delete(CategoryViewModel viewModel)
        {
            var categorySpec = new CategorySpecification(viewModel.Id);
            var category = await categoryRepository.GetSingleOrDefaultAsync(categorySpec);

            try
            {
                await categoryRepository.DeleteAsync(category);
            }
            catch (DbUpdateException exception)
            {
                ModelState.AddModelError("", "Category needs to be empty, to delete");
                return View(nameof(Index), viewModel);
            }


            return RedirectToAction(nameof(List));
        }

    }
}
