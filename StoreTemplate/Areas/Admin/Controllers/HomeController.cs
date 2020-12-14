using Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreTemplate.Areas.Constants;

namespace StoreTemplate.Areas.Admin.Controllers
{
    [Area(AreaNamesConstants.AdminAreaName)]
    [Authorize(Roles = IdentityRoleConstants.AdminRoleName + "," + IdentityRoleConstants.ManagerRoleName)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}