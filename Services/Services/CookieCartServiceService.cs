using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Infrastructure.Data.Repositories.Base;
using Microsoft.AspNetCore.Http;
using Services.Model;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CookieCartServiceService : ICartService
    {
        private readonly IRequestCookieCollection cookieCollection;
        private readonly IProductRepository productRepository;
        private const string CartCookieKey = "cart";
        
        private class CookieCartLine
        {
            public int ProductId { get; set; }
            public int Count { get; set; }
        }

        public CookieCartServiceService(IHttpContextAccessor httpContextAccessor, IProductRepository repository)
        {
            this.cookieCollection = httpContextAccessor.HttpContext.Request.Cookies;
            this.productRepository = repository;
        }

        public async Task<Cart> GetCartAsync()
        {
            var cartLines = GetCookieCartLines();
            var productIds = cartLines.Select(cartLine => cartLine.ProductId).ToList();
            var products = await productRepository.GetProductsByIds(productIds);

            var cartItems = products.Zip(cartLines,
                (product, line) => new CartItem() {Product = product, Quantity = line.Count}).ToList();

            return new Cart(cartItems);

        }

        private List<CookieCartLine> GetCookieCartLines()
        {
            var cookieCartLines = new List<CookieCartLine>();
            if (cookieCollection.TryGetValue(CartCookieKey, out var cartCookieString))
            {
                cartCookieString ??= "[]";
                cookieCartLines = JsonSerializer.Deserialize<List<CookieCartLine>>(cartCookieString);
            }

            return cookieCartLines;
        }
    }
}
