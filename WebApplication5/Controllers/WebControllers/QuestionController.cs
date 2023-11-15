using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;
using WebApplication5.ViewModels;
using WebApplication5.ViewModels.QuestionAndAnswer;

namespace WebApplication5.Controllers.WebControllers
{
    public class QuestionController : Controller
    {
        public ActionResult QuestionAnswer(int id)
        {
            QuestionAnswerViewModel viewModel = new QuestionAnswerViewModel();
            return View(viewModel);
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
        public ActionResult QuestionUpdate(QuestionCreateViewModel viewModel)
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
        public ActionResult QuestionDelete(QuestionCreateViewModel viewModel)
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
    }
}
