namespace WebApplication5.Models
{
    public class Answer
    {
        public bool IsAccepted { get; set; }

        public Question? AssociatedQuestion { get; set; }
    }
}
