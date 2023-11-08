namespace WebApplication5.Models
{
    public abstract class Post
    {
        public int Id { get; set; }

        public int Vote { get; set; }

        public string? Content { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
