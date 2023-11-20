using WebApplication5.Models;
using WebApplication5.ViewModels.QuestionAndAnswer;

namespace WebApplication5.ViewModels
{
	public class QuestionAnswerViewModel
	{
		public Question Question { get; set; }

		public List<Answer> Answers { get; set; }

		public Answer AnswerCreateForm { get; set; }

        public AnswerUpdateViewModel AnswerUpdateForm { get; set; }
    }
}
