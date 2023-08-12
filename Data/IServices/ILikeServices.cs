using Blog.Data.DTOs;

namespace Blog.Data.IServices
{
    public interface ILikeServices
    {
       public Task<bool> LikeBlog(LikeDTO like);
       public Task<bool> UnlikeBlog(LikeDTO like);
       public Task<bool> CheckLike(LikeDTO like);
       public Task<int> GetBlogLikes(int blogID);
    }
}
