using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASP.NETCoreD10.Controllers
{
    public class TestClaimsController : Controller
    {
        /*------------------------------------------------------------------*/
        [Authorize]
        public IActionResult Index()
        {
            // Read From Claims
            string name = User.Identity.Name;
            var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var nameFromClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            var claims = User.Claims.ToList();
            return Content("");
        }
        /*------------------------------------------------------------------*/
    }
}
