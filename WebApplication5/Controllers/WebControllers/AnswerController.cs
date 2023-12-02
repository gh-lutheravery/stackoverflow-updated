using Microsoft.AspNetCore.Mvc;
using WebApplication5.Controllers.BusinessControllers;
using WebApplication5.ViewModels.QuestionAndAnswer;
using WebApplication5.Controllers.WebControllers;
using Microsoft.AspNetCore.Authorization;
using Humanizer.Localisation;
using System.Security.Claims;
using WebApplication5.Models;

namespace WebApplication5.Controllers.WebControllers
{
    [Authorize]
    public class AnswerController : Controller
    {
        private readonly AnswerBusinessController _businessController;
        private readonly UserAuthorizer _userAuthorizer;
        public AnswerController(AnswerBusinessController businessController, UserAuthorizer userAuthorizer)
        {
            _businessController = businessController;
            _userAuthorizer = userAuthorizer;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnswerCreate(AnswerCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _businessController.SubmitAnswerForm(viewModel, User);
            }

            return RedirectToAction(nameof(QuestionController.Details), "Question",
                                    routeValues: new { id = viewModel.AssociatedQuestionId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnswerUpdate(AnswerUpdateViewModel viewModel)
        {
            if (!_userAuthorizer.IsUserTheAuthorByResourceId(viewModel.OriginalAnswerId, new Answer(), User))
                return Forbid();

            if (ModelState.IsValid)
            {
                _businessController.UpdateAnswer(viewModel);  
            }

            return RedirectToAction(nameof(QuestionController.Details), "Question",
                    routeValues: new { id = viewModel.AssociatedQuestionId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnswerDelete(int id)
        {
            if (!_userAuthorizer.IsUserTheAuthorByResourceId(id, new Answer(), User))
                return Forbid();

            _businessController.DeleteAnswer(id);
            return RedirectToAction(nameof(HomeController.Index), "Home");
            
        }
    }
}
