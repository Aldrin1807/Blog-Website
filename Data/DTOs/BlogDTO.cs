namespace Blog.Data.DTOs
{
    public class BlogDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile Image{ get; set; }
        public string Tags { get; set; } //Seperated by commas

        public string UserID { get; set; }

    }
}
