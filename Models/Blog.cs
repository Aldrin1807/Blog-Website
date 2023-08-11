namespace Blog.Models
{
    public class Blog
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public string ImagePath { get; set; }
        public string Tags { get; set; } //Seperated by commas


        //Relationships
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
    }
}
