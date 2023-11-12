namespace WebApplication5.Models
{
    public class Profile
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public string? PicturePath { get; set; }

        public string? Bio { get; set; }

        public DateTime? DateCreated { get; set; }

        public List<Question>? Questions { get; set; }

        public List<Answer>? Answers { get; set; }
    }
}
