using Microsoft.AspNetCore.Identity;

namespace Blog.Models
{
    public class ApplicationUser:IdentityUser
    {




        public List<Blog> Blogs{ get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
    }
}
