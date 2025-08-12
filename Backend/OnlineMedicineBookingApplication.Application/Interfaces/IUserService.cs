using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Application.Models.UserDTOS;
using OnlineMedicineBookingApplication.Domain.Entities;

namespace OnlineMedicineBookingApplication.Application.Interfaces
{
    // Interface for handling all user-related operations
    public interface IUserService
    {
        // Authenticates user credentials and returns the User object if valid
        Task<User> LoginAsync(UserDTO user);

        // Registers a new user with provided registration data
        Task RegisterAsync(UserRegisterDTO userRegisterDTO);

        // Retrieves a specific user's profile based on user ID
        Task<UserResponseDTO> GetUserProfileAsync(int id);

        // Returns a list of all users (typically for admin use)
        Task<List<User>> GetAllUsersAsync();

        // Deletes a user account by ID
        Task DeleteUserAsync(int id);

        // Updates user details and returns updated user info
        Task<UserResponseDTO> UpdateUserAsync(UserUpdateDTO user);

        // Resets the user's password
        Task ResetUserPasswordAsync(int userId, string oldPassword, string newPassword);
    }
}
