using Blog.Data.DTOs;
using Blog.Data.IServices;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Services
{
    public class LikeServices : ILikeServices
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public LikeServices(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<bool> CheckLike(LikeDTO like)
        {
            return await _context.Likes.AnyAsync(l=>l.UserID==like.UserID && l.BlogID == like.BlogID);
        }

        public async Task<int> GetBlogLikes(int blogID)
        {
            var likes =await _context.Likes.Where(l => l.BlogID == blogID).ToListAsync();
            return likes.Count;
        }

        public async Task<bool> LikeBlog(LikeDTO like)
        {
            var user =await _userManager.FindByIdAsync(like.UserID);
            var blog =await _context.Blogs.FirstOrDefaultAsync(b => b.ID == like.BlogID);
            if (user == null || blog == null)
            {
                throw new Exception("User or Blog does not exist");
            }
            var likeObj = new Like
            {
                BlogID = like.BlogID,
                UserID = like.UserID
            };
            await _context.Likes.AddAsync(likeObj);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnlikeBlog(LikeDTO like)
        {
            var _like  = await _context.Likes.FirstOrDefaultAsync(l => l.BlogID == like.BlogID && l.UserID == like.UserID);
            if (_like == null)
            {
                throw new Exception("Like does not exist");
            }
            _context.Likes.Remove(_like);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
