using WebApplication5.Controllers.DataServices;
using WebApplication5.ViewModels.Home;
using X.PagedList;

namespace WebApplication5.Controllers.BusinessControllers
{
    public class HomeBusinessController
    {
        private SOCloneContextService _context;
        private readonly int PageSize = 10;

        public HomeBusinessController(SOCloneContextService context) 
        { 
            _context = context;
        }

        // GET: HomeBusinessController
        public HomeViewModel PopulateHomeViewModel(int pageNumber)
        {
            HomeViewModel vm = new HomeViewModel();
            vm.Questions = _context.GetAllQuestions(true)
                .ToPagedList(pageNumber, PageSize);

            var tags = _context.GetAllTags(false)
                .Select(t => t.ToString());
            vm.RandomTags = RandomizeTags(tags).Take(10).ToList();
            return vm;
        }

        private IQueryable<string> RandomizeTags(IQueryable<string> tags)
        {
            Random rand = new Random();
            var shuffled = tags.OrderBy(a => rand.Next());
            return shuffled;
        }

        public SearchViewModel PopulateSearchViewModel(int pageNumber, string searchTerm)
        {
            SearchViewModel vm = new SearchViewModel();
            vm = (SearchViewModel)PopulateHomeViewModel(pageNumber);

            vm.Questions.Where(q => q.Title.Contains(searchTerm)).ToList();
            vm.SearchQuery = searchTerm;

            return vm;
        }
    }
}
