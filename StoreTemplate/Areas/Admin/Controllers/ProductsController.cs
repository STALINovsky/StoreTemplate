using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Constants;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Specifications.CategorySpecifications;
using Infrastructure.Specifications.ProductSpecifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Services.Services.Interfaces;
using StoreTemplate.Areas.Admin.Models;
using StoreTemplateCore.Entities;

namespace StoreTemplate.Areas.Admin.Controllers
{
    [Area(AreaNamesConstants.AdminAreaName)]
    [Authorize(Roles = IdentityRoleConstants.AdminRoleName + "," + IdentityRoleConstants.ManagerRoleName)]
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IImageService imageService;
        private const int TopProductCount = 9;

        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository, IImageService imageService)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.imageService = imageService;
        }


        public async Task<IActionResult> List(int page = 0)
        {
            var spec = new ProductSpecification().IncludeTags().SortByPopularity().AddPagination(TopProductCount, page);
            var products = await productRepository.GetAsync(spec);

            return View(products);
        }

        public async Task<IActionResult> Index(string name)
        {
            var spec = new ProductSpecification(name).IncludeCategory();
            var product = await productRepository.GetSingleOrDefaultAsync(spec);
            var productViewModel = ProductViewModel.InstanceByProduct(product);

            return View(productViewModel);
        }

        private async Task<Category> GetProductCategoryByViewModelOrDefault(ProductViewModel viewModel)
        {
            var categorySpecification = new CategorySpecification(viewModel.CategoryName);
            var productCategory = await categoryRepository.GetSingleOrDefaultAsync(categorySpecification);

            return productCategory;
        }

        private async Task<Product> UpdateProductByViewModel(Product product, ProductViewModel viewModel, Category productCategory)
        {

            product.Id = viewModel.Id;
            product.Name = viewModel.Name;
            product.Price = viewModel.Price;

            product.Description = viewModel.Description;
            product.Summary = viewModel.Summary;
            product.UnitsInStock = viewModel.UnitsInStock;
            product.CategoryId = productCategory.Id;


            if (viewModel.Image != null)
            {
                product.ImagePath = await imageService.SaveImage(product.GetType(), viewModel.Name, viewModel.Image);
            }

            return product;
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductViewModel viewModel)
        {

            var productCategory = await GetProductCategoryByViewModelOrDefault(viewModel);
            if (productCategory == null)
            {
                ModelState.AddModelError(nameof(ProductViewModel.CategoryName), "Invalid Category");
                return View(nameof(Index), viewModel);
            }
            var oldProduct = await productRepository.GetSingleOrDefaultAsync(new ProductSpecification(viewModel.Id));

            var product = await UpdateProductByViewModel(oldProduct, viewModel, productCategory);

            try
            {
                await productRepository.UpdateAsync(product);
            }
            catch (DbUpdateException exception)
            {
                if (viewModel.Image != null)
                {
                    imageService.DeleteImage(product.GetType(), viewModel.Name, viewModel.Image);
                }

                ModelState.AddModelError(nameof(viewModel.Name), "Name needs to be unique");
                return View(nameof(Index), viewModel);
            }

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View(new ProductViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel viewModel)
        {

            var productCategory = await GetProductCategoryByViewModelOrDefault(viewModel);
            if (productCategory == null)
            {
                ModelState.AddModelError(nameof(viewModel.CategoryName), "Invalid Category");
                return View(viewModel);
            }

            var productToAdd = await UpdateProductByViewModel(new Product(), viewModel, productCategory);
            try
            {
                await productRepository.AddAsync(productToAdd);
            }
            catch (DbUpdateException exception)
            {
                if (viewModel.Image != null)
                {
                    imageService.DeleteImage(productToAdd.GetType(), viewModel.Name, viewModel.Image);
                }

                ModelState.AddModelError(nameof(viewModel.Name), "Name needs to be unique");
                return View(viewModel);
            }

            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string name)
        {
            var productSpecification = new ProductSpecification(name);
            var product = await productRepository.GetSingleOrDefaultAsync(productSpecification);
            await productRepository.DeleteAsync(product);

            return RedirectToAction(nameof(List));
        }
    }
}
