﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using WebApplication5.Controllers.DataServices;
using WebApplication5.Models;
using WebApplication5.ViewModels;
using WebApplication5.ViewModels.Home;
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

        public ProfileViewModel PopulateProfileViewModel(int profileId)
        {
            ProfileViewModel vm = new ProfileViewModel();
            vm.CurrentProfile = _contextService.GetProfileWithQA(profileId, true);
            vm.UpdatedProfile = new ProfileUpdateViewModel();

            vm.UpdatedProfile.OriginalProfileId = profileId;
            return vm;
        }

        // if user is found, make a claim based on the user id and configure cookie settings
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
            Profile? profile = _contextService.context.Profile.Where(p => p.Email == vm.Email).FirstOrDefault();

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
            Profile? profile = _contextService.context.Profile.Find(vm.OriginalProfileId);

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
            Profile? profile = _contextService.context.Profile.Find(id);

            if (profile != null)
            {
                _contextService.context.Profile.Remove(profile);
                _contextService.context.SaveChanges();
            }
        }
    }
}
