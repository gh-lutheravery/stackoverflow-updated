using System.ComponentModel.DataAnnotations;
using WebApplication5.Controllers.BusinessControllers;
using WebApplication5.Models;

namespace WebApplication5.ViewModels.QuestionAndAnswer
{
    public class QuestionCreateViewModel : IValidatableObject
    {
        private QuestionBusinessController _businessController;
        public QuestionCreateViewModel(QuestionBusinessController businessController) 
        {
            _businessController = businessController;
        }

        public List<string> AllTags { get; set; }

        public List<string> Tags { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxPostTitleLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxPostContentLength)]
        public string Content { get; set; }

        public string TruncatedContent { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!ValidTags())
            {
                yield return new ValidationResult(
                    "One or more of your tags doesn't exist; try again."
                );
            }
        }

        public bool ValidTags()
        {
            if (_businessController.ValidateTagStrings(Tags))  
                return true; 
            else
                return false;
        }
    }
}
