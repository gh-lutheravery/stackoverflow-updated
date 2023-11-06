namespace WebApplication5.ViewModels.QuestionAndAnswer
{
	public class QuestionAnswerViewModel
	{
		public Question Question { get; set; }
		public List<Answer> Answers { get; set; }
		public Answer AnswerCreateForm { get; set; }

		public DeleteModalViewModel DeleteQuestionModel { get; set; }
		public DeleteModalViewModel DeleteAnswerModel { get; set; }
	}
}
