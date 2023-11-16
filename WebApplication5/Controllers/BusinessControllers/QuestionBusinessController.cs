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

            _contextService.CreateQuestion(newQuestion);
        }

        // POST: QuestionAndAnswerController/Create
        public void UpdateQuestion(QuestionCreateViewModel vm, int originalId)
        {
            Question originalQuestion = _contextService.GetQuestionById(originalId);

            // check if tags were updated to possibly avoid expensive func calls
            if (originalQuestion.Tags.Select(t => t.Title) != vm.Tags)
            {
                List<Tag> newTags = _contextService.GetAllTags(false)
                    .Where(t => vm.Tags.Contains(t.Title)).ToList();
            }
            
            originalQuestion.Title = vm.Title;
            originalQuestion.Content = vm.Content;
            originalQuestion.ViewCount = 0;
            originalQuestion.TruncatedContent = vm.TruncatedContent;
            originalQuestion.DateUpdated = DateTime.Now;

            _contextService.context.Question.Update(originalQuestion);
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
