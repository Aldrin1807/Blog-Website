using Blog.Data.DTOs;
using Blog.Models;

namespace Blog.Data.IServices
{
    public interface IAdminServices
    {
        public Task<List<ApplicationUser>> GetUsers();
        public Task<bool> DeleteUser(string userID);
        public Task<bool> UpdateUser(RegisterDTO user, string userID);

    }
}
