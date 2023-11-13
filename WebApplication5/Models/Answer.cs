using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Answer : Post
    {
        public bool IsAccepted { get; set; }

        [Required]
        public Question? AssociatedQuestion { get; set; }
    }
}
