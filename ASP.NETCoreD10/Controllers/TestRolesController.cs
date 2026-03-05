using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreD10.Controllers
{
    public class TestRolesController : Controller
    {
        /*------------------------------------------------------------------*/
        [Authorize]
        public IActionResult IndexV01()
        {
            return Content("Index V01");
        }
        /*------------------------------------------------------------------*/
        [Authorize(Roles = SystemRoles.User)]
        public IActionResult IndexV02()
        {
            return Content("Index User");
        }
        /*------------------------------------------------------------------*/
        [Authorize(Roles = SystemRoles.Admin)]
        public IActionResult IndexV03()
        {
            return Content("Index Admin");
        }
        /*------------------------------------------------------------------*/
        // Allow Admin OR User
        // In Case Plain Text
        [Authorize(Roles = "Admin,User")]

        // In Case of Const
        [Authorize(Roles = SystemRoles.Admin + "," + SystemRoles.User)]
        public IActionResult IndexV04()
        {
            return Content("Index Admin, User");
        }
        /*------------------------------------------------------------------*/
    }
}
