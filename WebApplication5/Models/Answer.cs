namespace WebApplication5.Models
{
    public class Answer : Post
    {
        public bool IsAccepted { get; set; }

        public Question? AssociatedQuestion { get; set; }
    }
}
