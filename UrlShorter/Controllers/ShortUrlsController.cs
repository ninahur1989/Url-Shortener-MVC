using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShorter.Data.Static;
using UrlShorter.Models;
using UrlShorter.Services.Interfaces;

namespace UrlShorter.Controllers
{
    [Authorize]
    public class ShortUrlsController : Controller
    {
        private readonly IShortUrlService _service;
        private readonly IEncodeService _encodeservice;

        public ShortUrlsController(IShortUrlService service, IEncodeService encodeservice)
        {
            _service = service;
            _encodeservice = encodeservice;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return RedirectToAction(actionName: nameof(Create));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string originalUrl)
        {
            if (_service.AlreadyExsist(originalUrl))
                return Content("this url already exsist, please check a table");

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var shortUrl = new ShortUrl
            {
                OriginalUrl = originalUrl,
                UserId = userId,
                CreatedDate = DateTime.UtcNow,
            };

            TryValidateModel(shortUrl);
            if (ModelState.IsValid)
            {
                _service.Save(shortUrl);

                return RedirectToAction(actionName: nameof(Show), routeValues: new { id = shortUrl.Id });
            }

            return View(shortUrl);
        }

        public IActionResult Show(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var shortUrl = _service.GetById(id.Value);
            if (shortUrl == null)
            {
                return NotFound();
            }

            ViewData["Path"] = _encodeservice.Encode(shortUrl.Id);

            return View(shortUrl);
        }

        [AllowAnonymous]
        public IActionResult ShowAll()
        {
            return View(_service.GetAll());
        }

        [HttpGet("/ShortUrls/RedirectTo/{path:required}", Name = "ShortUrls_RedirectTo")]
        public IActionResult RedirectTo(string path)
        {
            if (path == null)
            {
                return NotFound();
            }

            var shortUrl = _service.GetByPath(path);
            if (shortUrl == null)
            {
                return NotFound();
            }

            return Redirect(shortUrl.OriginalUrl);
        }

        public IActionResult DeleteUrl(int id)
        {
            var item = _service.GetById(id);
            if (_service.DeleteUrl(item))
                return RedirectToAction(actionName: "ShowAll");

            return Content("error delete");
        }

        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult DeleteAllUrls()
        {
            _service.DeleteAllUrls();
            return RedirectToAction(actionName: "ShowAll");
        }
    }
}
