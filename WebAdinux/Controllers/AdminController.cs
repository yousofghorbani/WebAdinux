using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAdinux.Context.Entities;
using WebAdinux.Context.Enums;
using WebAdinux.Core.Interfaces;
using WebAdinux.Core.ViewMoels;

namespace WebAdinux.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUser _user;

        public AdminController(IUser user)
        {
            _user = user;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            var res = await _user.FindUser(viewModel);
            if (res == null) 
            { 
                ModelState.AddModelError("Password", "Username or password is incorrect");
                return View(viewModel);
            }
            var claims = new List<Claim>()
            {
                            new Claim(ClaimTypes.NameIdentifier, res.Id.ToString()),
                            new Claim(ClaimTypes.Name, res.Email),
                            new Claim(ClaimTypes.Role, ((Role)res.Role).ToString())
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };
            await HttpContext.SignInAsync(principal, properties);
            return RedirectToAction("Index", "Admin");
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
