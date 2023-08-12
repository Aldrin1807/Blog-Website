using Blog.Data.DTOs;
using Blog.Data.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _services;

        public AuthController(IAuthServices services)
        {
                _services = services;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                var response =await _services.Login(login);
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            try
            {
                var response =await _services.Register(register);
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}
