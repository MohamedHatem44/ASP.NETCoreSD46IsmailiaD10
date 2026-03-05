using ASP.NETCoreD10.Models;
using ASP.NETCoreD10.ViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASP.NETCoreD10.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        /*------------------------------------------------------------------*/
        private readonly RoleManager<ApplicationRole> _roleManager;
        /*------------------------------------------------------------------*/
        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }
        /*------------------------------------------------------------------*/
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        /*------------------------------------------------------------------*/
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleVM createRoleVM)
        {
            if(!ModelState.IsValid)
            {
                return View(createRoleVM);
            }

            var ApplicationRole = new ApplicationRole
            {
                Name = createRoleVM.RoleName,
            };

            IdentityResult result = await _roleManager.CreateAsync(ApplicationRole);
            if (!result.Succeeded)
            {
                return View(createRoleVM);
            }

            return RedirectToAction("Index", "Home");
        }
        /*------------------------------------------------------------------*/
    }
}
