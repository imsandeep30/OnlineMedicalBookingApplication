using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models.UserDTOS;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserContract _userRepository;
        private readonly ICartContract _cartRepository;

        // Constructor - injecting user and cart repositories
        public UserService(IUserContract userRepository, ICartContract cartRepository)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }

        // Login method - checks if user with email and password exists
        public async Task<User> LoginAsync(UserDTO user)
        {
            return await _userRepository.GetUserByEmailAndPasswordAsync(user.UserEmail, user.UserPassword);
        }

        // Register a new user and create a default empty cart
        public async Task RegisterAsync(UserRegisterDTO user)
        {
            // Create new User entity
            var newUser = new User
            {
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                UserPassword = user.UserPassword,
                UserPhone = user.UserPhone,
                Role = user.Role,
            };

            // Add user to database
            await _userRepository.AddUserAsync(newUser);

            // Create default empty cart for the new user
            var newCart = new Cart
            {
                UserId = newUser.UserId,
                Items = new List<CartItem>(),
                TotalPrice = 0,
            };

            // Save the cart in the database
            await _cartRepository.DefaultCart(newCart);
        }

        // Get user profile details by user ID
        public Task<User> GetUserProfileAsync(int id) => _userRepository.GetUserByIdAsync(id);

        // Get all registered users
        public Task<List<User>> GetAllUsersAsync() => _userRepository.GetAllUsersAsync();

        // Delete a user by ID
        public Task DeleteUserAsync(int id) => _userRepository.DeleteUserAsync(id);

        // Update user information
        public async Task<UserResponseDTO> UpdateUserAsync(UserUpdateDTO userUpdateDTO)
        {
            if (userUpdateDTO == null)
            {
                throw new ArgumentNullException(nameof(userUpdateDTO), "User update data cannot be null.");
            }

            // Create User entity from the update DTO
            var requestedUser = new User
            {
                UserId = userUpdateDTO.UserId,
                UserName = userUpdateDTO.Name,
                UserPhone = userUpdateDTO.PhoneNumber,
            };

            // Update user in the database
            await _userRepository.UpdateUserAsync(requestedUser);

            // Return updated user data as a response DTO
            return new UserResponseDTO
            {
                userEmail = requestedUser.UserEmail,
                userName = requestedUser.UserName,
                userPhone = requestedUser.UserPhone,
            };
        }

        // Reset user's password
        public Task ResetUserPasswordAsync(int userId, string newPassword)
            => _userRepository.ResetPasswordAsync(userId, newPassword);
    }
}
