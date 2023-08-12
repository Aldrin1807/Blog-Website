using Blog.Data.DTOs;
using Blog.Data.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogController : ControllerBase
    {
        private readonly IBlogServices _services;

        public BlogController(IBlogServices services) 
        {
            _services = services;
        }

        [HttpPost("create-blog")]
        public async Task<IActionResult> CreateBlog([FromForm]BlogDTO blog)
        {
            try
            {
               var response = await _services.CreateBlog(blog);
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete-blog")]
        public async Task<IActionResult> DeleteBlog(int blogID)
        {
            try
            {
                var response = await _services.DeleteBlog(blogID);
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-blogs")]
        public async Task<IActionResult> GetBlogs()
        {
            try
            {
                var response = await _services.GetBlogs();
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update-blog")]
        public async Task<IActionResult> UpdateBlog([FromForm]BlogDTO blog,int blogId)
        {
            try
            {
                var response = await _services.UpdateBlog(blog,blogId);
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-users-blogs")]
        public async Task<IActionResult> GetUserBlogs(string userID)
        {
            try
            {
                var response = await _services.GetUsersBlogs(userID);
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }   
    }
}
