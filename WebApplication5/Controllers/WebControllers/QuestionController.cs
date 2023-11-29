using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Controllers.BusinessControllers;
using WebApplication5.Models;
using WebApplication5.ViewModels;
using WebApplication5.ViewModels.QuestionAndAnswer;

namespace WebApplication5.Controllers.WebControllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly QuestionBusinessController _questionBusinessController;
        private readonly TagBusinessController _tagBusinessController;
        private readonly UserAuthorizer _userAuthorizer;

        public QuestionController(QuestionBusinessController questionBusinessController, TagBusinessController tagBusinessController, UserAuthorizer userAuthorizer)
        {
            _questionBusinessController = questionBusinessController;
            _tagBusinessController = tagBusinessController;
            _userAuthorizer = userAuthorizer;
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            QuestionAnswerViewModel viewModel = _questionBusinessController.PopulateQuestionAnswerViewModel(id);
            return View("QuestionAnswer", viewModel);
        }


        public ActionResult QuestionCreate()
        {
            QuestionCreateViewModel vm = new QuestionCreateViewModel(_questionBusinessController);

            vm.AllTags = _tagBusinessController.GetAllTagStrings();
            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuestionCreate(QuestionCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _questionBusinessController.SubmitQuestionForm(viewModel, User);
                return RedirectToAction(nameof(Details));
            }
            else
            {
                return View(viewModel);
            }
        }

        public ActionResult QuestionUpdate(int id)
        {
            QuestionUpdateViewModel vm = new QuestionUpdateViewModel();
            vm.OriginalQuestion = _questionBusinessController.GetQuestion(id);
            vm.OriginalQuestionId = id;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuestionUpdate(QuestionUpdateViewModel viewModel)
        {
            if (!_userAuthorizer.IsUserTheAuthor(viewModel.OriginalQuestion.Id, User))
                return Forbid();

            if (ModelState.IsValid)
            {
                _questionBusinessController.UpdateQuestion(viewModel);
                return RedirectToAction(nameof(Details));
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
            if (!_userAuthorizer.IsUserTheAuthorByResourceId(questionId, new Question(), User))
                return Forbid();

            _questionBusinessController.DeleteQuestion(questionId);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
