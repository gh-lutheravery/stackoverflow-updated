using System.ComponentModel.DataAnnotations;

namespace WebApplication5.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password length must be more than 8 characters.")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Make sure your password confirmation matches your password.")]
        public string ConfirmPassword { get; set; }
    }
}
