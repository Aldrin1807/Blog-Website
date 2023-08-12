using Blog.Data.DTOs;
using Blog.Data.IServices;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Services
{
    public class BlogServices:IBlogServices
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public BlogServices(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CreateBlog(BlogDTO blogDTO)
        {
            var user = await _userManager.FindByIdAsync(blogDTO.UserID);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var blog = new Models.Blog
            {
                 Title = blogDTO.Title,
                 Content = blogDTO.Content,
                 DateTime = DateTime.Now,
                 UserID = blogDTO.UserID,
                 Tags = blogDTO.Tags
            };
            if (blogDTO.Image != null && blogDTO.Image.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + blogDTO.Image.FileName;
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Blog Images");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    blogDTO.Image.CopyTo(fileStream);
                }

                blog.ImagePath = uniqueFileName;
            }
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBlog(int blogId)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.ID == blogId);
            if (blog == null)
            {
                throw new Exception("Blog not found");
            }
            if (!string.IsNullOrEmpty(blog.ImagePath))
            {
                string ImageFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Blog Images", blog.ImagePath);
                if (File.Exists(ImageFilePath))
                {
                    File.Delete(ImageFilePath);
                }
            }
            var likes = await _context.Likes.Where(l => l.BlogID == blogId).ToListAsync();
            _context.Likes.RemoveRange(likes);
            var comments = await _context.Comments.Where(c => c.BlogPostId == blogId).ToListAsync();
            _context.Comments.RemoveRange(comments);



            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Models.Blog>> GetBlogs()=>await _context.Blogs.ToListAsync();

        public async Task<bool> UpdateBlog(BlogDTO blog,int blogID)
        {
            var _blog = await _context.Blogs.FirstOrDefaultAsync(b => b.ID == blogID);
            if (_blog == null)
            {
                throw new Exception("Blog not found");
            }
            _blog.Title = blog.Title;
            _blog.Content = blog.Content;
            _blog.Tags = blog.Tags;

            if (blog.Image != null && blog.Image.Length > 0)
            {
                if (!string.IsNullOrEmpty(_blog.ImagePath))
                {
                    string ImageFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Blog Images", _blog.ImagePath);
                    if (File.Exists(ImageFilePath))
                    {
                        File.Delete(ImageFilePath);
                    }
                }
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + blog.Image.FileName;
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Blog Images");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    blog.Image.CopyTo(fileStream);
                }

                _blog.ImagePath = uniqueFileName;
            }
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
