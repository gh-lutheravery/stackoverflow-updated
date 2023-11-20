using System.ComponentModel.DataAnnotations;
using WebApplication5.Models;

namespace WebApplication5.ViewModels.User
{
    public class ProfileUpdateViewModel : IValidatableObject
    {
        public Profile OriginalProfile { get; set; }

        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [FileExtensions]
        public string? PicturePath { get; set; }

        public string? Bio { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsTheSameAsOriginal())
            {
                yield return new ValidationResult(
                    "No profile change was detected; try again."
                );
            }
        }

        public bool IsTheSameAsOriginal()
        {
            bool equals = this.Name == OriginalProfile.Name && 
                this.Email == OriginalProfile.Email && 
                this.PicturePath == OriginalProfile.PicturePath &&
                this.Bio == OriginalProfile.Bio;

            return equals;
        }
    }
}
