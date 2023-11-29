using System.ComponentModel.DataAnnotations;

namespace WebApplication5.ViewModels.User
{
    public class ProfileUpdateViewModel
    {
        public int OriginalProfileId { get; set; }

        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [FileExtensions]
        public string? PicturePath { get; set; }

        public string? Bio { get; set; }
    }
}
