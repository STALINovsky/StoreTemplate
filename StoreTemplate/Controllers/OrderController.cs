using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data.Repositories.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using StoreTemplateCore.Entities;
using StoreTemplateCore.Identity;

namespace StoreTemplate.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICartService cartService;
        private readonly UserManager<User> userManager;
        public OrderController(IOrderRepository orderRepository, ICartService cartService, UserManager<User> userManager)
        {
            this.orderRepository = orderRepository;
            this.cartService = cartService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult AddOrder()
        {
            return View(new OrderDetails());
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderDetails orderDetails)
        {
            var cart = await this.cartService.GetCartAsync();

            var orderItems = cart.Items.Select(item => new OrderItem() { Count = item.Quantity, Product = item.Product }).ToList();
            var user = await userManager.GetUserAsync(this.User);
            var order = new Order()
            {
                OrderItems = orderItems,
                OrderDetails = orderDetails,
                CreateTime = DateTime.Now,
                User = user
            };
            await orderRepository.AddAsync(order);

            return LocalRedirect("~/");
        }
    }
}
