using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMedicineBookingApplication.Application.Models;
using OnlineMedicineBookingApplication.Application.Interfaces;
namespace OnlineMedicineBookingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO userModel)
        {
            if (userModel == null)
            {
                return BadRequest("User model cannot be null.");
            }
             await _userService.RegisterAsync(userModel);
            //if (result)
            //{
            //    return Ok("User registered successfully.");
            //}
            //return BadRequest("User registration failed.");
            return Ok("User registered successfully.");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserDTO userModel)
        {
            if (userModel == null)
            {
                return BadRequest("User model cannot be null.");
            }
            var user = await _userService.LoginAsync(userModel);
            if (user != null)
            {
                return Ok(user);
            }
            return Unauthorized("Invalid email or password.");
        }
        [HttpGet("Profile/{id}")]
        public async Task<IActionResult> GetUserProfile(int id)
        {
            var user = await _userService.GetUserProfile(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound("User not found.");
        }
        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            if (users != null && users.Count > 0)
            {
                return Ok(users);
            }
            return NotFound("No users found.");
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return Ok("User deleted successfully.");
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUser(UserUpdateDTO user)
        {
            if (user == null)
            {
                return BadRequest("User model cannot be null.");
            }
            await _userService.UpdateUser(user);
            return Ok("User updated successfully.");
        }
        [HttpGet("ResetPassword/{userId}/{newPassword}")]
        public async Task<IActionResult> ResetPassword(int userId, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                return BadRequest("New password cannot be empty.");
            }
            await _userService.ResetUserPassword(userId, newPassword);
            return Ok("User password reset successfully.");
        }
    }
}
