using WebApplication5.Models;

namespace WebApplication5.ViewModels.User
{
    public class ProfileViewModel
    {
        public Profile? CurrentProfile { get; set; }

        public ProfileUpdateViewModel UpdatedProfile { get; set; }
    }
}
