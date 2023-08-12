using Blog.Data.DTOs;
using Blog.Models;

namespace Blog.Data.IServices
{
    public interface IAuthServices
    {
        public Task<string> Login(LoginDTO login);
        public Task<bool> Register(RegisterDTO register);
        public Task<string> GenerateToken(ApplicationUser user);
    }
}
