using Microsoft.AspNetCore.Mvc;

namespace WebApplication5.Controllers.WebControllers
{
    // redirects the default ASP.NET auth paths; necessary because they can't seem to be changed in this version

    public class AccountController : Controller
    {
        public ActionResult Register()
        {
            return RedirectToAction("Register", "Profile");
        }

        public ActionResult Login()
        {
            return RedirectToAction("Login", "Profile");
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Logout", "Profile");
        }

        public ActionResult AccessDenied()
        {
            return RedirectToAction("Forbidden", "Error");
        }
    }
}
