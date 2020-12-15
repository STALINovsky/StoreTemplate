using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Specifications.ProductSpecifications;
using Services.Model;
using Services.Services.Interfaces;
using StoreTemplateCore.Entities;

namespace StoreTemplate.Controllers
{
    public class CartController : Controller
    {
        public ICartService CartService { get; }

        public CartController(ICartService cartService)
        {
            this.CartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var cart = await CartService.GetCartAsync();
            return View(cart.Items);
        }
    }
}
