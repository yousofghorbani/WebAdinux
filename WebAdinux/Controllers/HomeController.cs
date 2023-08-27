using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAdinux.Models;

namespace WebAdinux.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}