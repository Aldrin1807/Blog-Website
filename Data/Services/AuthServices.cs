using Blog.Data.DTOs;
using Blog.Data.IServices;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.Data.Services
{
    public class AuthServices:IAuthServices
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthServices(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration= configuration;
        }

        public async Task<string> Login(LoginDTO login)
        {
            
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
               throw new Exception("User not found");
            }
            var checkPassword = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!checkPassword)
            {
                throw new Exception("Password didn't match");
            }
            var token = await GenerateToken(user);

            return token;

        }

        public async Task<bool> Register(RegisterDTO register)
        {
            var emailExist =await _userManager.FindByEmailAsync(register.Email);
            var usernameExist =await _userManager.FindByNameAsync(register.Username);
            if (emailExist != null)
            {
                throw new Exception("Email already exists");
            }

            if (usernameExist != null)
            {
                throw new Exception("Username already exists");
            }

            await CreateRoleIfNotExists(UserRoles.User);
            await CreateRoleIfNotExists(UserRoles.Admin);


            var user = new ApplicationUser
            {
                Email = register.Email,
                UserName = register.Username
            };
            var result =await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                throw new Exception(result.ToString());
            }
            await _userManager.AddToRoleAsync(user, UserRoles.User);
            await _context.SaveChangesAsync();
            return true;

        }

        private async Task CreateRoleIfNotExists(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }


        public async Task<string> GenerateToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securitytoken = new JwtSecurityToken(
                claims: claims,
                expires:DateTime.Now.AddMinutes(50),
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                signingCredentials:creds
                );
            string token = new JwtSecurityTokenHandler().WriteToken(securitytoken);

            return token;
        }
    }
}
