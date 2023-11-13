using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Question : Post
    {
        [Required]
        public string? Title { get; set; }

        public List<Tag>? Tags { get; set; }

        public int ViewCount { get; set; }
    }
}
