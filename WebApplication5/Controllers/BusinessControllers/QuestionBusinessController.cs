using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication5.Controllers.DataServices;
using WebApplication5.Models;
using WebApplication5.ViewModels;
using WebApplication5.ViewModels.QuestionAndAnswer;

namespace WebApplication5.Controllers.BusinessControllers
{
    public class QuestionBusinessController
    {
        private SOCloneContextService _contextService;

        public QuestionBusinessController(SOCloneContextService context)
        {
            _contextService = context;
        }

        // GET: QuestionAndAnswerController
        public QuestionAnswerViewModel PopulateQuestionAnswerViewModel(int questionId)
        {
            QuestionAnswerViewModel vm = new QuestionAnswerViewModel();

            vm.Question = _contextService.GetQuestionById(questionId);  
            vm.Answers = _contextService.GetAllAnswers()
                .Where(a => a.AssociatedQuestion.Id == vm.Question.Id).ToList();
            vm.AnswerCreateForm = new Answer();
            vm.AnswerUpdateForm = new AnswerUpdateViewModel();

            return vm;
        }

        public void SubmitQuestionForm(QuestionCreateViewModel questionForm, ClaimsPrincipal userCookie)
        {
            Question newQuestion = new Question();

            List<Tag> newTags = _contextService.GetAllTags(false)
                .Where(t => questionForm.Tags.Contains(t.Title)).ToList();
            newQuestion.Tags = newTags;

            int userId = Int32.Parse(userCookie.FindFirstValue("ID"));
            newQuestion.Author = _contextService.GetProfileById(userId);

            newQuestion.Title = questionForm.Title;
            newQuestion.Content = newQuestion.Content;
            newQuestion.ViewCount = 0;
            newQuestion.TruncatedContent = questionForm.TruncatedContent;
            newQuestion.DateCreated = DateTime.Now;
            newQuestion.AcceptedAnswerId = null;
            newQuestion.AnswerCount = 0;

            _contextService.CreateQuestion(newQuestion);
        }

        // POST: QuestionAndAnswerController/Create
        public void UpdateQuestion(QuestionUpdateViewModel vm)
        {
            // check if tags were updated to possibly avoid expensive func calls
            if (vm.OriginalQuestion.Tags.Select(t => t.Title) != vm.Tags)
            {
                vm.OriginalQuestion.Tags = _contextService.GetAllTags(false)
                    .Where(t => vm.Tags.Contains(t.Title)).ToList();
            }

            vm.OriginalQuestion.Title = vm.Title;
            vm.OriginalQuestion.Content = vm.Content;
            vm.OriginalQuestion.ViewCount = 0;
            vm.OriginalQuestion.TruncatedContent = vm.TruncatedContent;
            vm.OriginalQuestion.DateUpdated = DateTime.Now;

            _contextService.context.Question.Update(vm.OriginalQuestion);
            _contextService.context.SaveChanges();
        }

        // GET: QuestionAndAnswerController/Edit/5
        public void DeleteQuestion(int id)
        {
            var question = _contextService.GetQuestionById(id);

            _contextService.context.Question.Remove(question);
            _contextService.context.SaveChanges();
        }

        public bool ValidateTagStrings(List<string> tags)
        {
            IQueryable<Tag> allTags = _contextService.context.Tag.AsNoTracking();
            IQueryable<string> allStringTags = allTags.Select(t => t.Title);

            if (tags.All(t => allStringTags.Contains(t)))
                return true;
            else
                return false;
        }

        public void IncrementAnswerCount(int id)
        {
            _contextService.context.Question.SingleOrDefault(a => a.Id == id).AnswerCount += 1;
            _contextService.context.SaveChanges();
        }
    }
}
