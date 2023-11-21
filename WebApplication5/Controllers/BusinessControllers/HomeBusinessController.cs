using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using WebApplication5.Controllers.DataServices;
using WebApplication5.Models;
using WebApplication5.ViewModels.Home;
using X.PagedList;

namespace WebApplication5.Controllers.BusinessControllers
{
    public class HomeBusinessController
    {
        private SOCloneContextService _context;
        private readonly int _pageSize = 10;

        private readonly string[] _sortTypes = new string[]
            { "Newest", "Unanswered" };

        private readonly string[] _filterTypes = new string[]
            { "NoAnswers", "NoAcceptedAnswers" };

        public HomeBusinessController(SOCloneContextService context) 
        { 
            _context = context;
        }


        public HomeViewModel PopulateHomeViewModel(int pageNumber, string sortBy, string filterBy)
        {
            HomeViewModel vm = new HomeViewModel();
            vm.Questions = _context.GetAllQuestions(true)
                .ToPagedList(pageNumber, _pageSize);

            var tags = _context.GetAllTags(false)
                .Select(t => t.ToString()).ToList();
            vm.RandomTags = RandomizeTags(tags).Take(10).ToList();

            if (!sortBy.IsNullOrEmpty() && _sortTypes.Contains(sortBy)) 
            {
                SortQuestions(vm, sortBy, pageNumber);
            }

            if (!filterBy.IsNullOrEmpty() && _filterTypes.Contains(filterBy))
            {
                FilterQuestions(vm, filterBy, pageNumber);
            }
            return vm;
        }


        private void SortQuestions(HomeViewModel vm, string sortBy, int pageNum)
        {
            switch (sortBy)
            {
                case "Newest":
                    vm.Questions = vm.Questions.OrderByDescending(q => q.DateCreated)
                        .ToPagedList(pageNum, _pageSize);
                    break;
                case "Unanswered":

                    break;
                default:
                    break;
            }
        }


        private void FilterQuestions(HomeViewModel vm, string filterBy, int pageNum)
        {
            switch (filterBy)
            {
                case "NoAnswers":
                    var noAnswerquestions = new List<Question>();
                    foreach (var question in vm.Questions)
                    {
                        if (!_context.context.Answer
                            .Where(a => a.AssociatedQuestion == question)
                            .Any())
                            noAnswerquestions.Add(question);
                    }

                    vm.Questions = noAnswerquestions.ToPagedList(pageNum, _pageSize);
                    break;

                case "NoAcceptedAnswers":
                    var noAccAnswerquestions = new List<Question>();
                    foreach (var question in vm.Questions)
                    {
                        if (_context.context.Answer
                            .Where(a => a.AssociatedQuestion == question && !a.IsAccepted)
                            .Any())
                            noAccAnswerquestions.Add(question);
                    }

                    vm.Questions = noAccAnswerquestions.ToPagedList(pageNum, _pageSize);
                    break;

                default:
                    break;
            }
        }

        private List<string> RandomizeTags(List<string> tags)
        {
            Random rand = new Random();
            var shuffled = tags.OrderBy(a => rand.Next()).ToList();
            return shuffled;
        }

        public SearchViewModel PopulateSearchViewModel(int pageNumber, string searchTerm)
        {
            SearchViewModel vm = new SearchViewModel();
            var homeView = PopulateHomeViewModel(pageNumber, "", "");


            vm.Questions = homeView.Questions.Where(q => q.Title.Contains(searchTerm)).ToPagedList();
            vm.SearchQuery = searchTerm;
            vm.RandomTags = homeView.RandomTags;

            return vm;
        }
    }
}
