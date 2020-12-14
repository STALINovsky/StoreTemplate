using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreTemplateCore.Identity;

namespace StoreTemplate.Areas.Admin
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private const int UserPerPage = 9;

        public AccountController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> List(int page = 1)
        {
            int usersToSkip = (page - 1) * UserPerPage;

            userManager.Users.Skip(usersToSkip).Take(UserPerPage);

            return View();
        }
    }
}
