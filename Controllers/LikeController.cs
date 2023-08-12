using Blog.Data.DTOs;
using Blog.Data.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LikeController : ControllerBase
    {
        private readonly ILikeServices _services;

        public LikeController(ILikeServices services)
        {
            _services = services;
        }



        [HttpPost("like-blog")]
        public async Task<IActionResult> LikeBlog([FromBody] LikeDTO like)
        {
            try
            {
                var response = await _services.LikeBlog(like);
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("unlike-blog")]
        public async Task<IActionResult> UnLikeBlog([FromBody]LikeDTO like)
        {
            try
            {
                var response = await _services.UnlikeBlog(like);
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("isliked")]
        public async Task<IActionResult> CheckLike(string UserID,int BlogID)
        {
            try
            {
                LikeDTO like = new LikeDTO()
                {
                    UserID = UserID,
                    BlogID = BlogID
                };
                var response = await _services.CheckLike(like);
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("get-likes")]
        public async Task<IActionResult> GetBlogLikes(int blogID)
        {
            try
            {
                var response = await _services.GetBlogLikes(blogID);
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
