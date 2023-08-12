using Blog.Data.DTOs;
using Blog.Data.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _services;

        public AdminController(IAdminServices services)
        {
            _services = services;
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers()
        {
              try
            {
                var response = await _services.GetUsers();
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(string userID)
        {
            try
            {
                var response = await _services.DeleteUser(userID);
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update-user")]    
        public async Task<IActionResult> UpdateUser([FromBody] RegisterDTO user,string userID)
        {
            try
            {
                var response = await _services.UpdateUser(user,userID);
                return Ok(response);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
