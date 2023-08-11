namespace Blog.Models
{
    public class Like
    {
        public string UserID { get;set; }
        public ApplicationUser User { get;set; }

        public int BlogID { get;set; }
        public Blog Blog { get;set; }
    }
}
