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
                Address = new Adress
                {
                    Street=user.UserAddress.UserStreet,
                    City=user.UserAddress.UserCity,
                    State=user.UserAddress.UserState,
                    ZipCode=user.UserAddress.UserZipCode,
                    Country=user.UserAddress.UserCountry,
                }, // Assuming Address is part of UserRegisterDTO
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
        public async Task<UserResponseDTO> GetUserProfileAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            var userDTO = new UserResponseDTO()
            {
                userId = user.UserId,
                userEmail = user.UserEmail,
                UserAddress = new AdressDTO
                {
                    UserStreet = user.Address.Street,
                    UserCity = user.Address.City,
                    UserState = user.Address.State,
                    UserZipCode = user.Address.ZipCode,
                    UserCountry = user.Address.Country
                },
                userName = user.UserName,
                userPhone = user.UserPhone
            };

            return userDTO;
        }


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
                Address = new Adress
                {
                    Street = userUpdateDTO.UserAddress.UserStreet,
                    City = userUpdateDTO.UserAddress.UserCity,
                    State = userUpdateDTO.UserAddress.UserState,
                    ZipCode = userUpdateDTO.UserAddress.UserZipCode,
                    Country = userUpdateDTO.UserAddress.UserCountry,
                }
            };

            // Update user in the database
            await _userRepository.UpdateUserAsync(requestedUser);

            // Return updated user data as a response DTO
            return new UserResponseDTO
            {
                userEmail = requestedUser.UserEmail,
                userName = requestedUser.UserName,
                userPhone = requestedUser.UserPhone,
                UserAddress = new AdressDTO
                {
                    UserStreet = requestedUser.Address.Street,
                    UserCity = requestedUser.Address.City,
                    UserState = requestedUser.Address.State,
                    UserZipCode = requestedUser.Address.ZipCode,
                    UserCountry = requestedUser.Address.Country
                },
            };
        }

        // Reset user's password
        public Task ResetUserPasswordAsync(PasswordResetDTO passwordReset)
        { 
            return _userRepository.ResetPasswordAsync(passwordReset.UserId, passwordReset.OldPassword, passwordReset.NewPassword);
        }
        //email searching
        public async Task<EmailSearchingDTO> searchMail(string Gmail)
        {
            var user = await _userRepository.SearchMail(Gmail);
            if (user == null)
            {
                return null;
            }
            return new EmailSearchingDTO
            {
                UserId = user.UserId,

                Name = user.UserName,

                Email = user.UserEmail,

                Password = user.UserPassword,
            };
        }
    }
}
