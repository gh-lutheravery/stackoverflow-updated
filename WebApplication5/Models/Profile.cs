using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication5.Models
{
    public class Profile
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Email { get; set; }

        public string? PicturePath { get; set; }

        public string? Bio { get; set; }

        [Required]
        public DateTime? DateCreated { get; set; }

        // prevent circular reference in res, since all posts have author property
        [JsonIgnore]
        public List<Question>? Questions { get; set; }

        [JsonIgnore]
        public List<Answer>? Answers { get; set; }
    }
}
