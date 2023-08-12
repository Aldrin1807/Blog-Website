using Blog.Data.DTOs;
using Blog.Models;

namespace Blog.Data.IServices
{
    public interface ICommentServices
    {
        public Task<bool> MakeComment(CommentDTO commentDTO);
        public Task<bool> DeleteComment(int commentId);
        public Task<List<Comment>> GetBlogComments(int blogID);
        public Task<bool> UpdateComment(CommentDTO comment, int commentID);

    }
}
