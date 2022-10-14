using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net6MVCCRUD.Access;
using net6MVCCRUD.Models;
using System.Diagnostics;

namespace net6MVCCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string CaptchaCode)
        {
            // 驗證驗證碼
            if (!Captcha.ValidateCaptchaCode(CaptchaCode, HttpContext))
            {
                HttpContext.Session.SetString("CaptchaCode", "Validation error");
                ModelState.AddModelError(string.Empty, "驗證碼錯誤");
                return RedirectToAction(nameof(Privacy));
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
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