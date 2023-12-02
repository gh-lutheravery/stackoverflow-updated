using System.ComponentModel.DataAnnotations;

namespace WebApplication5.ViewModels.QuestionAndAnswer
{
    public class AnswerUpdateViewModel
    {
        [MinLength(ValidationConstants.MinPostContentLength)]
        [MaxLength(ValidationConstants.MaxPostContentLength)]
        public string Content { get; set; }

        public int OriginalAnswerId { get; set; }

        public int AssociatedQuestionId { get; set; }

        public string? OriginalAnswerContent { get; set; }
    }
}
