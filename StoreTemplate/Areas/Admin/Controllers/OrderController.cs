using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Constants;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Specifications.OrderSpecifications;
using Microsoft.AspNetCore.Authorization;
using StoreTemplate.Areas.Constants;
using StoreTemplateCore.Entities;

namespace StoreTemplate.Areas.Admin.Controllers
{
    [Area(AreaNamesConstants.AdminAreaName)]
    [Authorize(Roles = IdentityRoleConstants.AdminRoleName + "," + IdentityRoleConstants.ManagerRoleName)]
    public class OrderController : Controller
    {
        private const int ItemsPerPage = 9;

        private readonly IOrderRepository orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<IActionResult> List(int page = 1)
        {
            var orderSpec = new OrderSpecification().AddPagination(ItemsPerPage, page);
            var orders = await orderRepository.GetAsync(orderSpec);
            return View(orders);
        }

        public async Task<IActionResult> Index(int id)
        {
            var orderSpec = new OrderSpecification(id).IncludeOrderItemsWithProducts().IncludeOrderDetails().OrderByDate();
            var order = await orderRepository.GetSingleOrDefaultAsync(orderSpec);
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Order order)
        {
            await orderRepository.UpdateAsync(order);
            return RedirectToAction(nameof(List));
        }

    }
}
