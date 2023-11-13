using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;
using WebApplication5.ViewModels;
using WebApplication5.ViewModels.QuestionAndAnswer;

namespace WebApplication5.Controllers.WebControllers
{
    public class QuestionAndAnswerController : Controller
    {
        public ActionResult QuestionAnswer(int id)
        {
            QuestionAnswerViewModel viewModel = new QuestionAnswerViewModel();
            return View(viewModel);
        }


        // post endpoint for submitting answers
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuestionAnswer(QuestionAnswerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(QuestionAnswer), viewModel.Question.Id);
            }
            else
            {
                return View(viewModel);
            }
        }


        public ActionResult QuestionCreate()
        {
            Question question = new Question();
            return View(question);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuestionCreate(QuestionCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(QuestionAnswer));
            }
            else
            {
                return View(viewModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnswerCreate(AnswerCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                
            }
           
            return RedirectToAction(nameof(QuestionAnswer), viewModel.Question.Id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
