using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreTemplate.ViewModels.Identity;
using StoreTemplateCore.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace StoreTemplate.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public SignInManager<User> SignInManager { get; set; }
        public UserManager<User> UserManager { get; set; }

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            returnUrl ??= "~/";
            return View(new RegisterModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            var user = await UserManager.FindByNameAsync(userModel.Name);

            if (user != null)
            {
                ModelState.AddModelError("", "this Name has already taken");
                return View(userModel);
            }

            var newUser = new User() { UserName = userModel.Name, Email = userModel.EMail };
            await UserManager.CreateAsync(newUser, userModel.Password);

            return LocalRedirect(userModel.ReturnUrl);
        }

        [HttpGet]
        public IActionResult LogIn(string returnUrl)
        {
            return View(new LogInModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInModel inModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inModel);
            }

            var user = await UserManager.FindByNameAsync(inModel.Name);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid user name");
                return View(inModel);
            }

            await SignInManager.SignOutAsync();
            var result = await SignInManager.PasswordSignInAsync(user, inModel.Password, false, false);

            if (result != SignInResult.Success)
            {
                ModelState.AddModelError("","Invalid PassWord");
                return View(inModel);
            }

            return LocalRedirect(inModel.ReturnUrl ?? "~/");
        }

        public async Task<LocalRedirectResult> LogOut(string returnUrl)
        {
            returnUrl ??= "~/";

            await SignInManager.SignOutAsync();
            return LocalRedirect("~/");
        }

    }
}
