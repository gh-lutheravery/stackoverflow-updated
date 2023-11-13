using Microsoft.AspNetCore.Mvc;
using WebApplication5.Controllers.DataServices;
using WebApplication5.Models;
using WebApplication5.ViewModels.User;

namespace WebApplication5.Controllers.WebControllers
{
    [Route("user")]
    public class ProfileController : Controller
    {
        public ProfileController()
        {
        }

        [Route("{id}")]
        public ActionResult Profile(int id)
        {
            if (profile != null)
                return View(profile);
            return NotFound();
        }

        public ActionResult Login()
        {
            LoginViewModel viewModel = new LoginViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Profile));
            }
            else
            {
                return View(vm);
            }
        }


        public ActionResult Register()
        {
            RegisterViewModel viewModel = new RegisterViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Login));
            }
            else
            {
                return View(vm);
            }
        }

        [Route("update")]
        public ActionResult ProfileUpdate(int id)
        {
            ProfileUpdateViewModel viewModel = new ProfileUpdateViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileUpdate(ProfileUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Profile));
            }
            else
            {
                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
