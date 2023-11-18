using System.ComponentModel.DataAnnotations;
using WebApplication5.Models;

namespace WebApplication5.ViewModels.QuestionAndAnswer
{
    public class QuestionUpdateViewModel
    {
        public List<string> Tags { get; set; }

        public string TagStr { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string TruncatedContent { get; set; }

        public Question? OriginalQuestion { get; set; }
    }
}
