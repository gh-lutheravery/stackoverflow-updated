using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Security.Claims;
using WebApplication5.Controllers.DataServices;
using WebApplication5.Models;
using WebApplication5.ViewModels;
using WebApplication5.ViewModels.Home;
using WebApplication5.ViewModels.QuestionAndAnswer;

namespace WebApplication5.Controllers.BusinessControllers
{
    public class QuestionBusinessController
    {
        private SOCloneContextService _context;

        public QuestionBusinessController(SOCloneContextService context)
        {
            _context = context;
        }

        // GET: QuestionAndAnswerController
        public QuestionAnswerViewModel PopulateQuestionAnswerViewModel(int questionId)
        {
            QuestionAnswerViewModel vm = new QuestionAnswerViewModel();

            vm.Question = _context.GetQuestionById(questionId);  
            vm.Answers = _context.GetAllAnswers()
                .Where(a => a.AssociatedQuestion.Id == vm.Question.Id).ToList();
            vm.AnswerCreateForm = new Answer();

            return vm;
        }

        public void SubmitQuestionForm(QuestionCreateViewModel questionForm, ClaimsPrincipal userCookie)
        {
            Question newQuestion = new Question();

            List<Tag> newTags = _context.GetAllTags(false)
                .Where(t => questionForm.Tags.Contains(t.Title)).ToList();

            int userId = Int32.Parse(userCookie.FindFirstValue("ID"));
            newQuestion.Author = _context.GetProfileById(userId);

            newQuestion.Title = questionForm.Title;
            newQuestion.Content = newQuestion.Content;
            newQuestion.ViewCount = 0;
            newQuestion.TruncatedContent = questionForm.TruncatedContent;
            newQuestion.DateCreated = DateTime.Now;

            _context.CreateQuestion(newQuestion);
        }

        // POST: QuestionAndAnswerController/Create
        public Question UpdateQuestion(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionAndAnswerController/Edit/5
        public ActionResult DeleteQuestion(int id)
        {
            return View();
        }
    }
}
