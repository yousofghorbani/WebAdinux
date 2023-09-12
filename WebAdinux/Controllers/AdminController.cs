using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAdinux.Context.Enums;
using WebAdinux.Core.Interfaces;
using WebAdinux.Core.ViewMoels;

namespace WebAdinux.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUser _user;
        private readonly IEmailMessage _emailMessage;
        private readonly ISiteHeader _siteHeader;
        private readonly ISiteContent _siteContent;

        public AdminController(IUser user, IEmailMessage emailMessage, ISiteHeader siteHeader, ISiteContent siteContent)
        {
            _user = user;
            _emailMessage = emailMessage;
            _siteHeader = siteHeader;
            _siteContent = siteContent;
        }

        #region Identity

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            if(HttpContext.User.Claims.Any())
            {
                return RedirectToAction("RecivedMails", "Admin");
            }
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
            return RedirectToAction("RecivedMails", "Admin");
        }
        #endregion

        #region Mail
        [Authorize]
        public async Task<IActionResult> RecivedMails()
        {
            var res = await _emailMessage.GetAll();
            return View(res);
        }

        [Authorize]
        public async Task<IActionResult> RecivedMailDetails([FromRoute] long id)
        {
            var res = await _emailMessage.GetById(id);
            return View(res);
        }
        #endregion

        #region Site Header

        [Authorize]
        public async Task<IActionResult> SiteHeaders()
        {
            var res = await _siteHeader.Filter(null);
            return View(res);
        }

        [Authorize]
        public IActionResult CreateHeader()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateHeader(SiteHeaderViewModel viewModel)
        {
            if(!ModelState.IsValid) return View(viewModel);

            viewModel.HasDropDown = true;
            var res = await _siteHeader.Add(viewModel);
            return Redirect("/Admin/SiteHeaders");
        }

        [Authorize]
        public async Task<IActionResult> EditHeader(long id)
        {
            return View(await _siteHeader.GetById(id));
        }

        [Authorize]
        public async Task<IActionResult> DeleteHeader(long id)
        {
            var res = await _siteHeader.Delete(id);
            return Redirect("/Admin/SiteHeaders");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditHeader(long id, SiteHeaderViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var res = await _siteHeader.Update(id ,viewModel);
            return Redirect("/Admin/SiteHeaders");
        }

        [Authorize]
        public IActionResult CreateSubHeader(long id)
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSubHeader(long id, SiteHeaderViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            viewModel.HasDropDown = false;
            viewModel.ParentId = id;
            var res = await _siteHeader.Add(viewModel);
            return Redirect("/Admin/SiteHeaders");
        }

        [Authorize]
        public async Task<IActionResult> EditSubHeader(long id)
        {
            return View(await _siteHeader.GetById(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditSubHeader(long id, SiteHeaderViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var res = await _siteHeader.Update(id, viewModel);
            return Redirect("/Admin/SiteHeaders");
        }

        #endregion

        #region Site Content



        #endregion
    }
}