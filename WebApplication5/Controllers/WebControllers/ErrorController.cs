using Microsoft.AspNetCore.Mvc;

namespace WebApplication5.Controllers.WebControllers
{
    public class ErrorController : Controller
    {
        [Route("not-found")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
