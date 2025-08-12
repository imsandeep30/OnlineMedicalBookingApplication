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
    // Controller to manage user operations like registration, login, profile, etc.
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;

        // Constructor to inject services
        public UserController(IUserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }

        // Register a new user
        [HttpPost("Register")]
        [AllowAnonymous] // This allows everyone to register
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userdto)
        {
            if (userdto == null)
            {
                return BadRequest("User cannot be null.");
            }

            await userService.RegisterAsync(userdto);
            return Ok("User registered successfully.");
        }

        // Login and get JWT token
        [HttpPost("Login")]
        [AllowAnonymous] // This allows everyone to login
        public async Task<IActionResult> Login([FromBody] UserDTO userlogin)
        {
            // Validate login input
            if (userlogin == null || string.IsNullOrEmpty(userlogin.UserEmail) || string.IsNullOrEmpty(userlogin.UserPassword))
            {
                return BadRequest("Email and Password are required");
            }

            // Authenticate user
            var Validateuser = await userService.LoginAsync(userlogin);
            if (Validateuser == null)
            {
                return NotFound("Invalid User!");
            }
            else
            {
                // Create and return JWT token
                var response = new UserResponseDTO
                {
                    userEmail = Validateuser.UserEmail,
                    userName = Validateuser.UserName,
                    userPhone = Validateuser.UserPhone,
                    userId = Validateuser.UserId,
                    Role = Validateuser.Role,
                    Token = GetToken(Validateuser)
                };
                return Ok(response);
            }
        }

        // Get user profile by ID
        [HttpGet("Profile/{id}")]
        [Authorize(Roles = "User,Admin")] // Both Admin and User can access this endpoint
        public async Task<IActionResult> GetUserProfile(int id)
        {
            var user = await userService.GetUserProfileAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound("User not found.");
        }

        // Get all registered users (Admin only)
        [HttpGet("AllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userService.GetAllUsersAsync();
            if (users != null && users.Count > 0)
            {
                return Ok(users);
            }
            return NotFound("No users found.");
        }

        // Delete a user by ID (User only)
        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await userService.DeleteUserAsync(id);
            return Ok("User deleted successfully.");
        }

        // Update user profile (User or Admin)
        //add both user and admin roles to update user details
        [HttpPut("Update")]
        [Authorize(Roles = "User,Admin")] // Both Admin and User can update user details
        public async Task<IActionResult> UpdateUser(UserUpdateDTO user)
        {
            if (user == null)
            {
                return BadRequest("User model cannot be null.");
            }
            await userService.UpdateUserAsync(user);
            return Ok("User updated successfully.");
        }

        // Reset password by userId
        [HttpGet("ResetPassword/{userId}/{olddPassword}/{newPassword}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> ResetPassword(int userId,string oldPassword,string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                return BadRequest("New password cannot be empty.");
            }
            await userService.ResetUserPasswordAsync(userId,oldPassword, newPassword);
            return Ok("User password reset successfully.");
        }

        // Method to generate JWT token for authenticated user
        private string GetToken(User? user)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);

            // Create signing credentials
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );

            // Define claims for the user
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
            };

            // Set token expiry
            var expires = DateTime.UtcNow.AddMinutes(10);

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials
            );

            // Serialize token to string
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenStr;
        }
    }
}
