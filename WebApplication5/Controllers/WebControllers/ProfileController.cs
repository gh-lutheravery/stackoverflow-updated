using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication5.Controllers.BusinessControllers;
using WebApplication5.Models;
using WebApplication5.ViewModels.User;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication5.Controllers.WebControllers
{
    public class ProfileController : Controller
    {
        private readonly ProfileBusinessController _businessController;
        private readonly UserAuthorizer _userAuthorizer;
        public ProfileController(ProfileBusinessController businessController, UserAuthorizer userAuthorizer)
        {
            _businessController = businessController;
            _userAuthorizer = userAuthorizer;
        }

        public ActionResult Details(int id)
        {
            var vm = _businessController.PopulateProfileViewModel(id);
            if (vm.CurrentProfile is null)
                return NotFound();

            return View("Profile", vm);
            
        }

        public ActionResult Login()
        {
            LoginViewModel viewModel = new LoginViewModel();
            return View("Login", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var loginInfo = _businessController.AuthenticateUser(vm, new PasswordHasher<Profile>());
                if (loginInfo == null)
                {
                    ViewData["IncorrectInput"] = "Your email or password is incorrect; try again.";
                    return View(vm);
                }

                var claims = loginInfo.Value.Item1;
                var authProperties = loginInfo.Value.Item2;

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claims),
                    authProperties);

                string userId = claims.Claims.Single().Value;
                
                return RedirectToAction(nameof(Details), routeValues: new { id = userId });
            }
            else
            {
                return View(vm);
            }
        }

        [Authorize]
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
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


        //[Authorize]
        //[Route("update")]
        //public ActionResult ProfileUpdate(int id)
        //{
        //    ProfileUpdateViewModel viewModel = new ProfileUpdateViewModel();
        //    return View(viewModel);
        //}

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileUpdate(ProfileViewModel vm)
        {
            if (!_userAuthorizer.IsUserTheAuthor(vm.UpdatedProfile.OriginalProfileId, User))
                return Forbid();

            if (ModelState.IsValid)
                _businessController.ProfileUpdate(vm.UpdatedProfile);
            
            return RedirectToAction(nameof(Details), routeValues: new { id = vm.UpdatedProfile.OriginalProfileId });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (!_userAuthorizer.IsUserTheAuthorByResourceId(id, new Profile(), User))
                return Forbid();

            _businessController.ProfileDelete(id);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
