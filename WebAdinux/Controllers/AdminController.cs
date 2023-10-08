using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration;
using System.Security.Claims;
using WebAdinux.Context.Entities;
using WebAdinux.Context.Enums;
using WebAdinux.Core.Interfaces;
using WebAdinux.Core.Security;
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
            var res = await _emailMessage.GetAll(false);
            return View(res);
        }
        [Authorize]
        public async Task<IActionResult> ArchivedMails()
        {
            var res = await _emailMessage.GetAll(true);
            return View(res);
        }

        [Authorize]
        public async Task<IActionResult> Archive(long id)
        {
            var res = await _emailMessage.Archive(id);
            if (res == false) return NotFound();
            return Redirect("/Admin/RecivedMails/");
        }
        [Authorize]
        public async Task<IActionResult> UnArchive(long id)
        {
            var res = await _emailMessage.UnArchive(id);
            if (res == false) return NotFound();
            return Redirect("/Admin/ArchivedMails/");
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
        public async Task<IActionResult> CreateHeader(SiteSubHeaderViewModel viewModel)
        {
            if(!ModelState.IsValid) return View(viewModel);

            SiteHeaderViewModel siteHeader = new SiteHeaderViewModel();
            siteHeader.Visible = viewModel.VisibleType == Visible.Visible ? true : false;
            siteHeader.HasDropDown = true;
            siteHeader.Title = viewModel.Title;
            
            var res = await _siteHeader.Add(siteHeader);
            return Redirect("/Admin/SiteHeaders");
        }

        [Authorize]
        public async Task<IActionResult> EditHeader(long id)
        {
            var res = await _siteHeader.GetById(id);

            SiteSubHeaderViewModel siteSubHeader = new SiteSubHeaderViewModel();
            siteSubHeader.Title = res.Title;
            siteSubHeader.VisibleType = res.Visible == true ? Visible.Visible : Visible.UnVisible;
            siteSubHeader.HasDropDown = res.HasDropDown;

            return View(siteSubHeader);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditHeader(long id, SiteSubHeaderViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            SiteHeaderViewModel siteHeader = new SiteHeaderViewModel();
            siteHeader.Visible = viewModel.VisibleType == Visible.Visible ? true : false;
            siteHeader.HasDropDown = true;
            siteHeader.Title = viewModel.Title;

            var res = await _siteHeader.Update(id, siteHeader);
            return Redirect("/Admin/SiteHeaders");
        }

        //[Authorize]
        //public async Task<IActionResult> DeleteHeader(long id)
        //{
        //    var res = await _siteHeader.Delete(id);
        //    return Redirect("/Admin/SiteHeaders");
        //}

        

        [Authorize]
        public IActionResult CreateSubHeader(long id)
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSubHeader(long id, SiteSubHeaderViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            
            
            SiteHeaderViewModel siteHeader = new SiteHeaderViewModel();
            siteHeader.Visible = viewModel.VisibleType == Visible.Visible ? true : false;
            siteHeader.HasDropDown = false;
            siteHeader.ParentId = id;
            siteHeader.Title = viewModel.Title;
            siteHeader.Link = viewModel.Link;
            var res = await _siteHeader.Add(siteHeader);
            return Redirect("/Admin/SiteHeaders");
        }

        [Authorize]
        public async Task<IActionResult> EditSubHeader(long id)
        {
            var res = await _siteHeader.GetById(id);

            SiteSubHeaderViewModel siteSubHeader = new SiteSubHeaderViewModel();
            siteSubHeader.Title = res.Title;
            siteSubHeader.Link = res.Link;
            siteSubHeader.VisibleType = res.Visible == true ? Visible.Visible : Visible.UnVisible;
            siteSubHeader.HasDropDown = res.HasDropDown;
            siteSubHeader.ParentId = res.ParentId;
            siteSubHeader.HasDropDown = res.HasDropDown;

            return View(siteSubHeader);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditSubHeader(long id, SiteSubHeaderViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            SiteHeaderViewModel siteHeader = new SiteHeaderViewModel();
            siteHeader.Visible = viewModel.VisibleType == Visible.Visible ? true : false;
            siteHeader.HasDropDown = false;
            siteHeader.ParentId = viewModel.ParentId;
            siteHeader.Title = viewModel.Title;
            siteHeader.Link = viewModel.Link;

            var res = await _siteHeader.Update(id, siteHeader);
            return Redirect("/Admin/SiteHeaders");
        }

        #endregion

        #region Site Content

        [Authorize]
        public async Task<IActionResult> GetHeaderContent(long id)
        {
            ViewBag.HeaderId = id;
            ViewBag.HeaderTitle = (await _siteHeader.GetById(id)).Title;
            try
            {
                var res = await _siteContent.GetByHeaderId(id);
                return View(res);
            }
            catch
            {
                return View(new List<GetSiteContentViewModel>());
            }
        }

        [Authorize]
        public async Task<IActionResult> GetContentById(long id)
        {
            var res = await _siteContent.GetById(id);
            return View(res);
        }

        [Authorize]
        public async Task<IActionResult> DeleteContent(long id)
        {
            var res = await _siteContent.Remove(id);
            if(res == 0) return NotFound();
            return Redirect("/Admin/GetHeaderContent/" + res);
        }

        [Authorize]
        public IActionResult CreateContent(long id)
        {
            ViewBag.HeaderId = id;
            return View();
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> CreateContent(long id, SiteContentViewModel viewModel)
        {
            if (viewModel.ContentType == ContentType.Img || viewModel.ContentType == ContentType.Video || viewModel.ContentType == ContentType.SliderImg || viewModel.ContentType == ContentType.LeftImageHtml || viewModel.ContentType == ContentType.RigthImageHtml)
            {
                if (viewModel.UploadFile != null)
                {
                    string filePath = "";
                    viewModel.FileLink = HashGenerators.FileCode() + Path.GetExtension(viewModel.UploadFile.FileName);
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/", viewModel.FileLink);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        viewModel.UploadFile.CopyTo(stream);
                    }
                }
            }

            viewModel.HeaderId = id;
            await _siteContent.Add(viewModel);

            return Redirect("/Admin/GetHeaderContent/" + id);
        }

        [Authorize]
        public async Task<IActionResult> EditContent(long id)
        {
            var res = await _siteContent.GetById(id);
            return View(res);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> EditContent(long id, SiteContentViewModel viewModel)
        {
            var content = await _siteContent.GetById(id);
            if (viewModel.ContentType == ContentType.Img || viewModel.ContentType == ContentType.Video || viewModel.ContentType == ContentType.SliderImg || viewModel.ContentType == ContentType.LeftImageHtml || viewModel.ContentType == ContentType.RigthImageHtml)
            {
                if (viewModel.UploadFile != null)
                {
                    string filePath = "";
                    viewModel.FileLink = HashGenerators.FileCode() + Path.GetExtension(viewModel.UploadFile.FileName);
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/", viewModel.FileLink);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        viewModel.UploadFile.CopyTo(stream);
                    }
                }
            }

            viewModel.HeaderId = id;
            await _siteContent.Update(id, viewModel);

            return Redirect("/Admin/GetHeaderContent/" + content.HeaderId);
        }

        [Authorize]
        public async Task<IActionResult> ContentDetails(long id)
        {
            var res = await _siteContent.GetById(id);
            return View(res);
        }

        #endregion
    }
}