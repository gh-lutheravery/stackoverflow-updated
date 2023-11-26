using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication5.Controllers.DataServices;
using WebApplication5.Models;
using WebApplication5.ViewModels;
using WebApplication5.ViewModels.QuestionAndAnswer;
using WebApplication5.ViewModels.User;

namespace WebApplication5.Controllers.BusinessControllers
{
    public class ProfileBusinessController
    {
        private SOCloneContextService _contextService;

        public ProfileBusinessController(SOCloneContextService context)
        {
            _contextService = context;
        }

        // GET: QuestionAndAnswerController
        public Profile? GetProfileWithQA(int? id)
        {
            if (id == null) 
                return null;

            Profile? profile = _contextService.context.Profile
                .Include(p => p.Questions)
                .ThenInclude(q => q.Tags)
                .Include(p => p.Answers)
                .SingleOrDefault(p => p.Id == id);

            return profile;
        }

        public (ClaimsIdentity, AuthenticationProperties)? 
            AuthenticateUser(LoginViewModel vm, PasswordHasher<Profile> hasher)
        { 
            var profile = ValidateLogin(vm, hasher);
            if (profile == null)
                return null;

            var claims = new List<Claim>
            {
                new Claim("ID", profile.Id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                IsPersistent = false,
                RedirectUri = ""
            };

            return (claimsIdentity, authProperties);
        }

        public Profile? ValidateLogin(LoginViewModel vm, PasswordHasher<Profile> hasher)
        {
            Profile? profile = _contextService.context.Profile
                .SingleOrDefault(p => p.Email == vm.Email);

            if (profile == null)
                return null;

            var result = hasher.VerifyHashedPassword(new Profile(), profile.Password, vm.Password);
            if (result == PasswordVerificationResult.Success)
                return profile;
            else
                return null;
        }

        
        public void Register(RegisterViewModel vm, PasswordHasher<Profile> hasher)
        {
            string hashed = hasher.HashPassword(new Profile(), vm.Password);

            Profile profile = new Profile();
            profile.Name = vm.Name;
            profile.Email = vm.Email;
            profile.Password = hashed;
            profile.DateCreated = DateTime.UtcNow;

            _contextService.context.Profile.Add(profile);
            _contextService.context.SaveChanges();
        }

        // GET: QuestionAndAnswerController/Edit/5
        public void ProfileUpdate(ProfileUpdateViewModel vm)
        {
            Profile? profile = _contextService.context.Profile
                .SingleOrDefault(p => p.Id == vm.OriginalProfile.Id);

            if (profile != null)
            {
                profile.Name = vm.Name;
                profile.Email = vm.Email;
                profile.PicturePath = vm.PicturePath;
                profile.Bio = vm.Bio;

                _contextService.context.Profile.Update(profile);
                _contextService.context.SaveChanges();
            }
        }

        public void ProfileDelete(int id)
        {
            Profile? profile = _contextService.context.Profile
                .SingleOrDefault(p => p.Id == id);

            if (profile != null)
            {
                _contextService.context.Profile.Remove(profile);
                _contextService.context.SaveChanges();
            }
        }
    }
}
