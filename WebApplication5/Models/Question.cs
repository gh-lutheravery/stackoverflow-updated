namespace WebApplication5.Models
{
    public class Question : Post
    {
        public string? Title { get; set; }

        public List<Tag>? Tags { get; set; }

        public int ViewCount { get; set; }

        // content truncated to 170 text chars max
        public string? TruncatedContent { get; set; }
    }
}
