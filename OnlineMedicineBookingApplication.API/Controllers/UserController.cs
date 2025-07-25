using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models.UserDTOS;
using OnlineMedicineBookingApplication.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace OnlineMedicineBookingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;
        public UserController(IUserService userService,IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }
        [HttpPost("Register")]
        [AllowAnonymous]// This allows everyone to register
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userdto)
        {
            if (userdto == null)
            {
                return BadRequest("User cannot be null.");
            }
             await userService.RegisterAsync(userdto);
            return Ok("User registered successfully.");
        }
        [HttpPost("Login")]
        [AllowAnonymous] // This allows everyone to login
        public async Task<IActionResult> Login([FromBody] UserDTO userlogin)
        {
            if (userlogin == null || string.IsNullOrEmpty(userlogin.UserEmail) || string.IsNullOrEmpty(userlogin.UserPassword))
            {
                return BadRequest("Email and Password are required");
            }
            var Validateuser = await userService.LoginAsync(userlogin);
            if (Validateuser == null)
            {
                return NotFound("Invalid User!");
            }
            else
            {
                var response = new UserResponseDTO
                {
                    //Here you can generate a JWT token and return it in the response
                    userEmail = Validateuser.UserEmail,
                    userName = Validateuser.UserName,
                    userPhone = Validateuser.UserPhone,
                    userId = Validateuser.UserId,
                    Role = Validateuser.Role,
                    Token = GetToken(Validateuser) // Generate JWT token for the user
                };
                return Ok(response);
            }
        }
        [HttpGet("Profile/{id}")]
        [Authorize(Roles ="User,Admin")] // Both Admin and User can access this endpoint
        public async Task<IActionResult> GetUserProfile(int id)
        {
            var user = await userService.GetUserProfileAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound("User not found.");
        }
        [HttpGet("AllUsers")]
        [Authorize(Roles="Admin")] // Only Admin can access this endpoint
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userService.GetAllUsersAsync();
            if (users != null && users.Count > 0)
            {
                return Ok(users);
            }
            return NotFound("No users found.");
        }
        [HttpDelete("Delete/{id}")]
        [Authorize(Roles ="User")] // Only User can access this endpoint
        public async Task<IActionResult> DeleteUser(int id)
        {
            await userService.DeleteUserAsync(id);
            return Ok("User deleted successfully.");
        }
        [HttpPut("Update")]
        [Authorize(Roles = "User,Admin")] // Both Admin,User can update user details
        public async Task<IActionResult> UpdateUser(UserUpdateDTO user)
        {
            if (user == null)
            {
                return BadRequest("User model cannot be null.");
            }
            await userService.UpdateUserAsync(user);
            return Ok("User updated successfully.");
        }
        [HttpGet("ResetPassword/{userId}/{newPassword}")]
        [Authorize(Roles ="User,Admin")]
        public async Task<IActionResult> ResetPassword(int userId, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                return BadRequest("New password cannot be empty.");
            }
            await userService.ResetUserPasswordAsync(userId, newPassword);
            return Ok("User password reset successfully.");
        }
        private string GetToken(User? user)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            //header part
            // // Create a signing key using the symmetric security key
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );
            //payload part
            // Create claims for the user
            var claims = new[]
            {
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(ClaimTypes.Role, user.Role),
                    };
            // Set the expiration time for the token
            var expires = DateTime.UtcNow.AddMinutes(10);
            //signature part
            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials
            );
            // Serialize the token to a string
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenStr;
        }
    }
}
