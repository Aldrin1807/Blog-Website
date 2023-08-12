using System.Text.Json.Serialization;

namespace Blog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public int BlogPostId { get; set; }

        [JsonIgnore]
        public Blog Blog { get; set; }
        public string? UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser? User { get; set; }
    }
}
