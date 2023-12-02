using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Quill.Delta;
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

        public QuestionAnswerViewModel PopulateQuestionAnswerViewModel(int questionId)
        {
            QuestionAnswerViewModel vm = new QuestionAnswerViewModel();

            var question = GetQuestionWithHtml(questionId);  
            vm.Question = question;

            var answers = _contextService.GetAllAnswers()
                .Where(a => a.AssociatedQuestion.Id == vm.Question.Id).ToList();

            for (int index = 0; index < answers.Count; index++)
            {
                answers[index].Content = ConvertQuillDeltaToHtml(answers[index].Content);
            }

            vm.Answers = answers;
            vm.AnswerCreateForm = new AnswerCreateViewModel();
            vm.AnswerCreateForm.AssociatedQuestionId = vm.Question.Id;

            vm.AnswerUpdateForm = new AnswerUpdateViewModel();
            vm.AnswerUpdateForm.AssociatedQuestionId = vm.Question.Id;

            return vm;
        }

        public Question GetQuestionWithHtml(int questionId)
        {
            var question = _contextService.GetQuestionById(questionId);
            question.Content = ConvertQuillDeltaToHtml(question.Content);

            return question;
        }


        public string ConvertQuillDeltaToHtml(string deltaString)
        {
            // convert quill "delta" string in question.Content to html to be rendered in view
            JObject deltaObj = JObject.Parse(deltaString);
            JArray deltaArr = (JArray)deltaObj["ops"];
            HtmlConverter htmlConverter = new HtmlConverter(deltaArr);

            return htmlConverter.Convert();
        }

        public int SubmitQuestionForm(QuestionCreateViewModel questionForm, ClaimsPrincipal userCookie)
        {
            Question newQuestion = new Question();

            List<Tag> newTags = _contextService.context.Tag
                .Where(t => questionForm.Tags.Contains(t.Title))
                .ToList();

            newQuestion.Tags = newTags;

            int userId = Int32.Parse(userCookie.FindFirstValue("ID"));
            newQuestion.Author = _contextService.context.Profile.Find(userId);

            newQuestion.Title = questionForm.Title;
            newQuestion.Content = questionForm.Content;
            newQuestion.ViewCount = 0;
            newQuestion.TruncatedContent = questionForm.TruncatedContent;
            newQuestion.DateCreated = DateTime.Now;
            newQuestion.AcceptedAnswerId = null;
            newQuestion.AnswerCount = 0;

            _contextService.CreateQuestion(newQuestion);
            return newQuestion.Id;
        }

        public void UpdateQuestion(QuestionUpdateViewModel vm)
        {
            var originalQuestion = _contextService.GetQuestionById(vm.OriginalQuestionId);
            originalQuestion.Title = vm.Title;
            originalQuestion.Content = vm.Content;
            originalQuestion.DateUpdated = DateTime.Now;

            // check if tags were updated to possibly avoid expensive func call
            if (!originalQuestion.Tags.Select(t => t.Title).SequenceEqual(vm.Tags))
            {
                originalQuestion.Tags = _contextService.context.Tag
                    .Where(t => vm.Tags.Contains(t.Title))
                    .ToList();
            }

            _contextService.context.Question.Update(originalQuestion);
            _contextService.context.SaveChanges();
        }

        public void DeleteQuestion(int id)
        {
            var question = _contextService.context.Question.Find(id);

            _contextService.context.Question.Remove(question);
            _contextService.context.SaveChanges();
        }

        public void IncrementAnswerCount(int id, int incrementBy)
        {
            var question = _contextService.context.Question.Find(id);
            question.AnswerCount += incrementBy;

            _contextService.context.Update(question);
            _contextService.context.SaveChanges();
        }

        public bool IncrementVoteCount(int id, int incrementBy)
        {
            var question = _contextService.context.Question.Find(id);
            if (question != null)
            {
                question.VoteCount += incrementBy;
                _contextService.context.Update(question);
                _contextService.context.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
