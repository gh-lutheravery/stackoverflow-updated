using Microsoft.AspNetCore.Mvc;
using WebApplication5.Controllers.BusinessControllers;
using WebApplication5.Models;
using WebApplication5.ViewModels;
using WebApplication5.ViewModels.QuestionAndAnswer;

namespace WebApplication5.Controllers.WebControllers
{
    public class QuestionController : Controller
    {
        private readonly QuestionBusinessController _businessController;
        public QuestionController(QuestionBusinessController businessController)
        {
            _businessController = businessController;
        }

        [Route("{id}")]
        public ActionResult QuestionAnswer(int id)
        {
            QuestionAnswerViewModel viewModel = _businessController.PopulateQuestionAnswerViewModel(id);
            return View(viewModel);
        }


        public ActionResult QuestionCreate()
        {
            QuestionCreateViewModel vm = new QuestionCreateViewModel(_businessController);
            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuestionCreate(QuestionCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _businessController.SubmitQuestionForm(viewModel, User);
                return RedirectToAction(nameof(QuestionAnswer));
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuestionUpdate(QuestionUpdateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _businessController.UpdateQuestion(viewModel);
                return RedirectToAction(nameof(QuestionAnswer));
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuestionDelete(int questionId)
        {
            _businessController.DeleteQuestion(questionId);
            return RedirectToAction(nameof(Index));
        }
    }
}
