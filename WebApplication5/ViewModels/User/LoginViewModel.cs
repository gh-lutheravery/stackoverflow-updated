using System.ComponentModel.DataAnnotations;

namespace WebApplication5.ViewModels.User
{
	public class LoginViewModel
	{
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
	}
}
