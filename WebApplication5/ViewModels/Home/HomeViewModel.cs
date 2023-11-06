using WebApplication5.Models.Question;

namespace WebApplication5.ViewModels.Home
{
	public class HomeViewModel
	{
		public string QuestionsTitle { get; set; }

		public List<Question> Questions { get; set; }

		public List<string> RandomTags { get; set; }
	}
}
