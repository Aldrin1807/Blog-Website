using Blog.Data.DTOs;
using Blog.Data.IServices;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminServices(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> DeleteUser(string userID)
        {
            var user =await _userManager.FindByIdAsync(userID);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            var likes =await _context.Likes.Where(l => l.UserID == userID).ToListAsync();
            var blogs =await _context.Blogs.Where(b => b.UserID == userID).ToListAsync();
            var comments =await _context.Comments.Where(c => c.UserId == userID).ToListAsync();
            _context.Likes.RemoveRange(likes);
            
            foreach(var blog in blogs)
            {
                var blogLikes =await _context.Likes.Where(l => l.BlogID == blog.ID).ToListAsync();
                var blogComments =await _context.Comments.Where(c => c.BlogPostId == blog.ID).ToListAsync();
                _context.Likes.RemoveRange(blogLikes);
                _context.Comments.RemoveRange(blogComments);
            }

            _context.Comments.RemoveRange(comments);

            await _userManager.DeleteAsync(user);
            return true;
        }

        public async Task<List<ApplicationUser>> GetUsers()=> await _userManager.Users.ToListAsync();

        public async Task<bool> UpdateUser(RegisterDTO user, string userID)
        {
            var _user = await _userManager.FindByIdAsync(userID);
            if(_user==null)
            {
                throw new Exception("User does not exist");
            }
            _user.Email = user.Email;
            _user.UserName = user.Username;
            _user.PasswordHash = _userManager.PasswordHasher.HashPassword(_user, user.Password);

            await _userManager.UpdateAsync(_user);
            await _context.SaveChangesAsync();   
            return true;
        }
    }
}
