using Microsoft.AspNetCore.Mvc;
using WebApplication5.Controllers.BusinessControllers;
using WebApplication5.ViewModels.QuestionAndAnswer;
using WebApplication5.Controllers.WebControllers;

namespace WebApplication5.Controllers.WebControllers
{
    public class AnswerController : Controller
    {
        private readonly AnswerBusinessController _businessController;
        public AnswerController(AnswerBusinessController businessController)
        {
            _businessController = businessController;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnswerCreate(AnswerCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _businessController.SubmitAnswerForm(viewModel, User);
                return RedirectToAction(viewModel.AssociatedQuestion.Id.ToString(), nameof(QuestionController));
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnswerUpdate(AnswerUpdateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _businessController.UpdateAnswer(viewModel);
                return RedirectToAction(nameof(QuestionController.Details), nameof(QuestionController),
                    routeValues: new { Id = viewModel.AssociatedQuestion.Id });
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnswerDelete(int id)
        {
            _businessController.DeleteAnswer(id);
            return RedirectToAction(nameof(HomeController.Index));
        }
    }
}
