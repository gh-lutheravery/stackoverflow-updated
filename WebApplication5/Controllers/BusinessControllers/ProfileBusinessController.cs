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
        public Profile? GetProfileWithQA(int id)
        {
            Profile? profile = _contextService.context.Profile
                .Include(q => q.Questions)
                .Include(a => a.Answers)
                .SingleOrDefault(p => p.Id == id);

            return profile;
        }

        public ClaimsIdentity? AuthenticateUser(LoginViewModel vm, PasswordHasher<Profile> hasher)
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

            return claimsIdentity;
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

        // POST: QuestionAndAnswerController/Create
        public void Register(QuestionCreateViewModel vm, int originalId)
        {
            string hashed = hasher.HashPassword(new Profile(), vm.Password);

            //Question originalQuestion = _contextService.GetQuestionById(originalId);

            //// check if tags were updated to possibly avoid expensive func calls
            //if (originalQuestion.Tags.Select(t => t.Title) != vm.Tags)
            //{
            //    List<Tag> newTags = _contextService.GetAllTags(false)
            //        .Where(t => vm.Tags.Contains(t.Title)).ToList();
            //}
            
            //originalQuestion.Title = vm.Title;
            //originalQuestion.Content = vm.Content;
            //originalQuestion.ViewCount = 0;
            //originalQuestion.TruncatedContent = vm.TruncatedContent;
            //originalQuestion.DateUpdated = DateTime.Now;

            _contextService.context.Profile.Add(originalQuestion);
            _contextService.context.SaveChanges();
        }

        // GET: QuestionAndAnswerController/Edit/5
        public void DeleteQuestion(int id)
        {
            var question = _contextService.GetQuestionById(id);

            _contextService.context.Question.Remove(question);
            _contextService.context.SaveChanges();
        }
    }
}
