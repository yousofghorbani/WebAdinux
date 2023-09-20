﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAdinux.Core.Interfaces;
using WebAdinux.Core.ViewMoels;
using WebAdinux.Models;

namespace WebAdinux.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailMessage _email;
        private readonly ISiteContent _content;
        public HomeController(IEmailMessage email, ISiteContent content)
        {
            _email = email;
            _content = content;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Route("/Adilyze")]
        public IActionResult Adilyze()
        {
            return View();
        }
        [Route("/Adicamp")]
        public IActionResult Adicamp()
        {
            return View();
        }
        [Route("/Adibox")]
        public IActionResult Adibox()
        {
            return View();
        }
        [Route("/Adishop")]
        public IActionResult Adishop()
        {
            return View();
        }
        [Route("/Adishow")]
        public IActionResult Adishow()
        {
            return View();
        }
        [Route("/Adimag")]
        public IActionResult Adimag()
        {
            return View();
        }
        [Route("/Adiroom")]
        public IActionResult Adiroom()
        {
            return View();
        }
        [Route("/Adimatch")]
        public IActionResult Adimactch()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(EmailMessageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _email.Add(viewModel);
                return RedirectToAction("MailSended", "Home");
            }
            return View(viewModel);
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult MailSended()
        {
            return View();
        }
        [Route("/Headers/{id}/{header}")]
        public async Task<IActionResult> HeaderContent(long id, string header)
        {
            List<GetSiteContentViewModel> contents = await _content.GetByHeaderId(id);
            if (contents.Any()) return View(contents);
            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}