using WebApplication5.Models;
using X.PagedList;

namespace WebApplication5.ViewModels.Home
{
	public class HomeViewModel
	{
		public IPagedList<Question> Questions { get; set; }

		public List<string> RandomTags { get; set; }
	}
}
