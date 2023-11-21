using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication5.Controllers.BusinessControllers;
using WebApplication5.Models;
using WebApplication5.ViewModels.User;

namespace WebApplication5.Controllers.WebControllers
{
    public class ProfileController : Controller
    {
        private readonly ProfileBusinessController _businessController;
        public ProfileController(ProfileBusinessController businessController)
        {
            _businessController = businessController;
        }

        [Route("{id}")]
        public ActionResult Profile(int id)
        {
            var profile = _businessController.GetProfileWithQA(id);
            if (profile is null)
                return NotFound();

            return View(profile);
            
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
                var loginInfo = _businessController.AuthenticateUser(vm, new PasswordHasher<Profile>());
                if (loginInfo == null)
                    return View(vm);

                var claims = loginInfo.Value.Item1;
                var authProperties = loginInfo.Value.Item2;

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claims),
                    authProperties);

                return RedirectToAction(nameof(Profile));
            }
            else
            {
                return View(vm);
            }
        }

        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
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
                _businessController.Register(vm, new PasswordHasher<Profile>());
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
                _businessController.ProfileUpdate(vm);
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
            _businessController.ProfileDelete(id);
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
        }
    }
}
