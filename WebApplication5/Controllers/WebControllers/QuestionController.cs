using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Controllers.BusinessControllers;
using WebApplication5.Migrations;
using WebApplication5.Models;
using WebApplication5.ViewModels;
using WebApplication5.ViewModels.QuestionAndAnswer;

namespace WebApplication5.Controllers.WebControllers
{
    public class VoteRequest
    {
        public int id { get; set; }
        public int incrementBy { get; set; }
    }

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
            QuestionCreateViewModel vm = new QuestionCreateViewModel();

            vm.AllTags = _tagBusinessController.GetAllTagStrings();
            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuestionCreate(QuestionCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int newId = _questionBusinessController.SubmitQuestionForm(viewModel, User);
                return RedirectToAction(nameof(Details), routeValues: new { id = newId });
            }
            else
            {
                return View(viewModel);
            }
        }

        public ActionResult QuestionUpdate(int id)
        {
            QuestionUpdateViewModel vm = new QuestionUpdateViewModel();
            vm.AllTags = _tagBusinessController.GetAllTagStrings();
            vm.OriginalQuestion = _questionBusinessController.GetQuestionWithHtml(id);
            vm.OriginalQuestionId = id;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuestionUpdate(QuestionUpdateViewModel viewModel)
        {
            if (!_userAuthorizer.IsUserTheAuthorByResourceId(viewModel.OriginalQuestionId, new Question(), User))
                return Forbid();

            if (ModelState.IsValid)
            {
                _questionBusinessController.UpdateQuestion(viewModel);
                return RedirectToAction(nameof(Details), routeValues: new { id = viewModel.OriginalQuestionId });
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QuestionDelete(int id)
        {
            if (!_userAuthorizer.IsUserTheAuthorByResourceId(id, new Question(), User))
                return Forbid();

            _questionBusinessController.DeleteQuestion(id);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        public ActionResult UpdateVote(string req)
        {
            var splitReq = req.Split(',');
            int id = Int32.Parse(splitReq[0]);
            int incrementBy = Int32.Parse(splitReq[1]);

            bool result = _questionBusinessController.IncrementVoteCount(id, incrementBy);
            if (result == false)
                return NotFound();

            return Ok();
        }
    }
}
