using System.ComponentModel.DataAnnotations;
using WebApplication5.Models;

namespace WebApplication5.ViewModels.QuestionAndAnswer
{
    public class QuestionCreateViewModel
    {
        public Profile OriginalProfile { get; set; }

        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(8)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [FileExtensions]
        public string? PicturePath { get; set; }

        public string? Bio { get; set; }
    }
}
