using Microsoft.AspNetCore.Mvc;
using WebApplication5.Controllers.BusinessControllers;
using WebApplication5.ViewModels.Home;

namespace WebApplication5.Controllers.WebControllers
{
    public class HomeController : Controller
    {
        private readonly HomeBusinessController _businessController;
        public HomeController(HomeBusinessController businessController) 
        { 
            _businessController = businessController;
        }

        public ActionResult Index(int pageNumber)
        {
            HomeViewModel vm = _businessController.PopulateHomeViewModel(pageNumber);
            return View(vm);
        }

        public ActionResult SearchResults(string searchQuery, int pageNumber)
        {
            SearchViewModel vm = _businessController.PopulateSearchViewModel(pageNumber, searchQuery);
            return View(vm);
        }
    }
}
