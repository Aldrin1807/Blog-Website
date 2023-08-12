using Blog.Data.DTOs;
using Blog.Data.IServices;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog.Data.Services
{
    public class CommentServices:ICommentServices
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public CommentServices(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> DeleteComment(int commentId)
        {
           var comment = await _context.Comments.FirstOrDefaultAsync(c=>c.Id == commentId);
            if (comment == null)
            {
                throw new Exception("Comment does not exist");
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true; 
        }

        public async Task<List<Comment>> GetBlogComments(int blogID)
        {
            var blog =await _context.Blogs.FirstOrDefaultAsync(b => b.ID == blogID);
            if (blog == null)
            {
                throw new Exception("Blog does not exist");
            }
            List<Comment> comments = _context.Comments.Where(c => c.BlogPostId == blogID).ToList();
            return comments;
        }

        public async Task<bool> MakeComment(CommentDTO commentDTO)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.ID == commentDTO.BlogPostId);
            var user = await _userManager.FindByIdAsync(commentDTO.UserId);

           if(blog == null || user == null)throw new Exception("User or Blog does not exist");
            var comment = new Comment
            {
                BlogPostId = commentDTO.BlogPostId,
                Content = commentDTO.Content,
                CommentDate = DateTime.Now,
                UserId = commentDTO.UserId
            };
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return true;    
        }

        public async Task<bool> UpdateComment(CommentDTO comment, int commentID)
        {
            var _comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentID);
            if(_comment == null)
            {
                throw new Exception("Comment does not exist");
            }
            _comment.CommentDate = DateTime.Now;
            _comment.Content = comment.Content;
            return true;
        }
    }
}
