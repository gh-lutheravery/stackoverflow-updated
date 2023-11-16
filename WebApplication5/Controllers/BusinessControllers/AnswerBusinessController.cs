using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication5.Controllers.DataServices;
using WebApplication5.Models;
using WebApplication5.ViewModels.QuestionAndAnswer;

namespace WebApplication5.Controllers.BusinessControllers
{
    public class AnswerBusinessController : Controller
    {
        private SOCloneContextService _contextService;

        public AnswerBusinessController(SOCloneContextService context)
        {
            _contextService = context;
        }

        public void SubmitAnswerForm(AnswerCreateViewModel answerForm, ClaimsPrincipal userCookie)
        {
            Answer newAnswer = new Answer();

            int userId = Int32.Parse(userCookie.FindFirstValue("ID"));
            newAnswer.Author = _contextService.GetProfileById(userId);

            newAnswer.Content = answerForm.Content;
            newAnswer.TruncatedContent = answerForm.TruncatedContent;
            newAnswer.DateCreated = DateTime.Now;
            newAnswer.AssociatedQuestion = answerForm.AssociatedQuestion;

            _contextService.context.Answer.Add(newAnswer);
        }

        // POST: AnswerAndAnswerController/Create
        public void UpdateAnswer(AnswerCreateViewModel vm, int originalId)
        {
            Answer originalAnswer = _contextService.context.Answer
                .Include(a => a.Author)
                .Include(a => a.AssociatedQuestion)
                .SingleOrDefault(a => a.Id == originalId);

            // check if tags were updated to possibly avoid expensive func calls
            originalAnswer.Content = vm.Content;
            originalAnswer.TruncatedContent = vm.TruncatedContent;
            originalAnswer.DateUpdated = DateTime.Now;

            _contextService.context.Answer.Update(originalAnswer);
            _contextService.context.SaveChanges();
        }

        // GET: AnswerAndAnswerController/Edit/5
        public void DeleteAnswer(int id)
        {
            var question = _contextService.context.Answer.SingleOrDefault(a => a.Id == id);

            _contextService.context.Answer.Remove(question);
            _contextService.context.SaveChanges();
        }
    }
}
