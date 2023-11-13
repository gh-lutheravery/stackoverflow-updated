using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public abstract class Post
    {
        public int Id { get; set; }

        [Required]
        public string? Content { get; set; }

        public string? TruncatedContent { get; set; }

        public Profile? Author { get; set; }

        public int VoteCount { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
