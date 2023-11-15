using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Controllers.DataServices;
using WebApplication5.Models;
using WebApplication5.ViewModels;

namespace WebApplication5.Controllers.BusinessControllers
{
    public class AnswerBusinessController : Controller
    {
        private SOCloneContextService _context;

        public AnswerBusinessController(SOCloneContextService context)
        {
            _context = context;
        }

        public void SubmitAnswerForm()
        {
            return View();
        }

        public Answer UpdateAnswer(Answer answer)
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

        // GET: AnswerAndAnswerController/Edit/5
        public void DeleteAnswer(int id)
        {
            return View();
        }
    }
}
