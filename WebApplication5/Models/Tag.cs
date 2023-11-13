using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication5.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        // prevent circular reference in res, since related questions have tags property
        [JsonIgnore]
        public List<Question>? Questions { get; set; }
    }
}
