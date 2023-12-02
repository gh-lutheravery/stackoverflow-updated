using System.ComponentModel.DataAnnotations;
using WebApplication5.Controllers.BusinessControllers;
using WebApplication5.Models;

namespace WebApplication5.ViewModels.QuestionAndAnswer
{
    public class QuestionCreateViewModel
    {
        public List<string>? AllTags { get; set; }

        public List<string> Tags { get; set; }

        [MinLength(ValidationConstants.MinPostTitleLength)]
        [MaxLength(ValidationConstants.MaxPostTitleLength)]
        public string Title { get; set; }

        [MinLength(ValidationConstants.MinPostContentLength)]
        [MaxLength(ValidationConstants.MaxPostContentLength)]
        public string Content { get; set; }

        public string TruncatedContent { get; set; }
    }
}
