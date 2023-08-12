using Blog.Data.DTOs;

namespace Blog.Data.IServices
{
    public interface IBlogServices
    {
        public Task<bool> CreateBlog(BlogDTO blogDTO);
        public Task<bool> DeleteBlog(int blogId);
        public Task<List<Models.Blog>> GetBlogs();
        public Task<List<Models.Blog>> GetUsersBlogs(string userID);

        public Task<bool> UpdateBlog(BlogDTO blog, int blogID);

    }
}
