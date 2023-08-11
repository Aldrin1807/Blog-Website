namespace Blog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public int BlogPostId { get; set; }
        public Blog Blog { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
