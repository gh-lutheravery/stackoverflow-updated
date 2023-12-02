using System.ComponentModel.DataAnnotations;
using WebApplication5.Models;

namespace WebApplication5.ViewModels.QuestionAndAnswer
{
    public class QuestionUpdateViewModel
    {
        public string[] Tags { get; set; }

        public List<string>? AllTags { get; set; }

        [MinLength(ValidationConstants.MinPostTitleLength)]
        [MaxLength(ValidationConstants.MaxPostTitleLength)]
        public string Title { get; set; }

        [MinLength(ValidationConstants.MinPostContentLength)]
        [MaxLength(ValidationConstants.MaxPostContentLength)]
        public string Content { get; set; }

        public string TruncatedContent { get; set; }

        public int OriginalQuestionId { get; set; }

        public Question? OriginalQuestion { get; set; }
    }
}
