using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication5.Controllers.WebControllers
{
    public class AnswerController : Controller
    {
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
        public ActionResult AnswerUpdate(AnswerCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

            }

            return RedirectToAction(nameof(QuestionAnswer), viewModel.Question.Id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnswerDelete(int id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
