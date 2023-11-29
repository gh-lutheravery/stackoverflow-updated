using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using WebApplication5.Controllers.DataServices;
using WebApplication5.Models;
using WebApplication5.ViewModels.QuestionAndAnswer;

namespace WebApplication5.Controllers.BusinessControllers
{
    public class AnswerBusinessController : Controller
    {
        private SOCloneContextService _contextService;
        private QuestionBusinessController _questionBusiness;
        public AnswerBusinessController(SOCloneContextService context, QuestionBusinessController questionBusiness)
        {
            _contextService = context;
            _questionBusiness = questionBusiness;
        }

        public void SubmitAnswerForm(AnswerCreateViewModel answerForm, ClaimsPrincipal userCookie)
        {
            Answer newAnswer = new Answer();

            int userId = Int32.Parse(userCookie.FindFirstValue("ID"));
            newAnswer.Author = _contextService.context.Profile.Find(userId);

            newAnswer.Content = answerForm.Content;
            newAnswer.TruncatedContent = answerForm.TruncatedContent;
            newAnswer.DateCreated = DateTime.Now;

            var associatedQuestion = _contextService.context.Question.Find(answerForm.AssociatedQuestionId);
            newAnswer.AssociatedQuestion = associatedQuestion;

            _contextService.context.Answer.Add(newAnswer);
            _contextService.context.SaveChanges();

            _questionBusiness.IncrementAnswerCount(answerForm.AssociatedQuestionId);
        }

        // POST: AnswerAndAnswerController/Create
        public void UpdateAnswer(AnswerUpdateViewModel vm)
        {
            Answer answer = _contextService.context.Answer.Find(vm.OriginalAnswerId);

            answer.Content = vm.Content;
            answer.DateUpdated = DateTime.Now;

            _contextService.context.Answer.Update(answer);
            _contextService.context.SaveChanges();
        }

        // GET: AnswerAndAnswerController/Edit/5
        public void DeleteAnswer(int id)
        {
            var question = _contextService.context.Answer.Find(id);

            _contextService.context.Answer.Remove(question);
            _contextService.context.SaveChanges();
        }
    }
}
