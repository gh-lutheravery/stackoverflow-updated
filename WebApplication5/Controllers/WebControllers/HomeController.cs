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

        public ActionResult Index(int pageNumber = 0, string sortBy = "", string filterBy = "")
        {
            HomeViewModel vm = _businessController.PopulateHomeViewModel(pageNumber, sortBy, filterBy);
            return View(vm);
        }

        public ActionResult SearchResults(string searchQuery, int pageNumber = 0)
        {
            SearchViewModel vm = _businessController.PopulateSearchViewModel(pageNumber, searchQuery);
            return View(vm);
        }
    }
}
