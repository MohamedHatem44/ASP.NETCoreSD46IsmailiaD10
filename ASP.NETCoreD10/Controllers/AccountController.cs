using ASP.NETCoreD10.Models;
using ASP.NETCoreD10.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;

namespace ASP.NETCoreD10.Controllers
{
    public class AccountController : Controller
    {
        /*------------------------------------------------------------------*/
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        /*------------------------------------------------------------------*/
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        /*------------------------------------------------------------------*/
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        /*------------------------------------------------------------------*/
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            // Map RegisterVM to ApplicationUser
            var applicationUser = new ApplicationUser
            {
                FirstName = registerVM.FirstName,
                UserName = registerVM.Email.Split('@')[0],
                LastName = registerVM.LastName,
                Email = registerVM.Email,
                //PasswordHash = registerVM.Password XXXX Just Plain Text
            };

            IdentityResult result = await _userManager.CreateAsync(applicationUser, registerVM.Password);
            if (!result.Succeeded)
            {
                // Error
                foreach (var errorItem in result.Errors)
                {
                    ModelState.AddModelError("", errorItem.Description);
                }
                return View(registerVM);
            }
            // Add Default Role To User
            IdentityResult addRoleResult = await _userManager.AddToRoleAsync(applicationUser, SystemRoles.Admin);
            if (!addRoleResult.Succeeded)
            {
                foreach (var errorItem in addRoleResult.Errors)
                {
                    ModelState.AddModelError("", errorItem.Description);
                }
                return View(registerVM);
            }
            //await _signInManager.SignInAsync(user, loginVM.RememberMe); // In Case Of Login In Register Action
            // Successfully registered user
            return RedirectToAction("Login");
        }
        /*------------------------------------------------------------------*/
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        /*------------------------------------------------------------------*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            ApplicationUser? user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Email or Passowrd");
                return View(loginVM);
            }

            //bool result = await _userManager.CheckPasswordAsync(user, loginVM.Password);
            //if (!result)
            //{
            //    ModelState.AddModelError("", "Invalid Email or Passowrd");
            //    return View(loginVM);
            //}

            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Email or Passowrd");
                return View(loginVM);
            }

            // Premisions
            //var res = await _userManager.AddClaimAsync() // Read True Create True // DB

            var claims = new List<Claim> // Not in DB
            {
                new Claim("LoginTime", DateTime.Now.ToString())
            };
           await _signInManager.SignInWithClaimsAsync(user, loginVM.RememberMe, claims);
   
            //await _signInManager.SignInAsync(user, loginVM.RememberMe); // In Case Of Login In Register Action

            return RedirectToAction("Index", "Home");
        }
        /*------------------------------------------------------------------*/
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        /*------------------------------------------------------------------*/
    }
}
