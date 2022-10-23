using Microsoft.AspNetCore.Mvc;
using UrlShorter.Models;
using UrlShorter.Services.Interfaces;

namespace UrlShorter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _service;

        public HomeController(IHomeService homeService)
        {
            _service = homeService;
        }

        public IActionResult Index()
        {
            return RedirectToAction(controllerName: "ShortUrls", actionName: "ShowAll");
        }

        public IActionResult About()
        {
            return View(_service.GetAbout());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult About(AboutAlgoritm algoritm)
        {
            _service.UpdateAbout(algoritm);
            return View(_service.GetAbout());
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
