using Microsoft.AspNetCore.Mvc;
using WebApplication5.ViewModels.Home;

namespace WebApplication5.Controllers.WebControllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeViewModel vm = new HomeViewModel();
            return View(vm);
        }

        public ActionResult SearchResults(string searchQuery)
        {
            SearchViewModel vm = new SearchViewModel();
            return View(vm);
        }
    }
}
