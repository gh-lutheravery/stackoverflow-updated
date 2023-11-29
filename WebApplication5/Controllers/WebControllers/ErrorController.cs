using Microsoft.AspNetCore.Mvc;

namespace WebApplication5.Controllers.WebControllers
{
    public class ErrorController : Controller
    {
        public IActionResult PageNotFound()
        {
            return View();
        }

        public IActionResult ServerError()
        {
            return View();
        }

        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
