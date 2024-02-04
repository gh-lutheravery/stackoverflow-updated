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

        public ActionResult LandingPage() 
        {
            return View();
        }

        public ActionResult Index(int pageNumber = 1, string sortBy = "", string filterBy = "")
        {
            HomeViewModel vm = _businessController.PopulateHomeViewModel(pageNumber, sortBy, filterBy);
            return View(vm);
        }

        public ActionResult Search(string searchQuery, string sortBy = "", int pageNumber = 1)
        {
            SearchViewModel vm = _businessController.PopulateSearchViewModel(pageNumber, searchQuery, sortBy);

            // SearchQuery string used in Layout view for search bar
            ViewData["SearchQuery"] = vm.SearchQuery;

            return View(vm);
        }
    }
}
