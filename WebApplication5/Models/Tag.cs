namespace WebApplication5.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int QuestionCount { get; set; }

        public List<Question>? Questions { get; set; }
    }
}
