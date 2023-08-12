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
    public class CommentController : ControllerBase
    {
        private readonly ICommentServices _services;

        public CommentController(ICommentServices services)
        {
            _services = services;
        }
        [HttpGet("getblogcomments")]
        public async Task<IActionResult> GetBlogComments(int blogID)
        {
            try
            {
                var response = await _services.GetBlogComments(blogID);
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("make-comment")]
        public async Task<IActionResult> MakeComment([FromBody] CommentDTO comment)
        {
            try
            {
                var response = await _services.MakeComment(comment);
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete-comment")]
        public async Task<IActionResult> DeleteComment(int commentID)
        {
            try
            {
                var response = await _services.DeleteComment(commentID);
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update-comment")]
        public async Task<IActionResult> UpdateComment([FromBody] CommentDTO comment, int commentID)
        {
            try
            {
                var response = await _services.UpdateComment(comment, commentID);
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
