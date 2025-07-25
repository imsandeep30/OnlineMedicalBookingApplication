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
    public class UserService: IUserService
    {
        private readonly IUserContract _userRepository;
        private readonly ICartContract _cartRepository;

        public UserService(IUserContract userRepository,ICartContract cartRepository)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }

        public async Task<User> LoginAsync(UserDTO user)
        {
            return await _userRepository.GetUserByEmailAndPasswordAsync(user.UserEmail, user.UserPassword);
        }

        public async Task RegisterAsync(UserRegisterDTO user)
        {
            var newUser = new User
            {
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                UserPassword = user.UserPassword,
                UserPhone = user.UserPhone,
                Role = user.Role,
            };
            await _userRepository.AddUserAsync(newUser);
            var newCart = new Cart
            {
                UserId = newUser.UserId,
                Items = new List<CartItem>(),
                TotalPrice = 0,
            };
            await _cartRepository.DefaultCart(newCart);
        }
        public Task<User> GetUserProfileAsync(int id) => _userRepository.GetUserByIdAsync(id);

        public Task<List<User>> GetAllUsersAsync() => _userRepository.GetAllUsersAsync();

        public Task DeleteUserAsync(int id) => _userRepository.DeleteUserAsync(id);

        public async Task<UserResponseDTO> UpdateUserAsync(UserUpdateDTO userUpdateDTO)
        {
            if (userUpdateDTO == null)
            {
                throw new ArgumentNullException(nameof(userUpdateDTO), "User update data cannot be null.");
            }
            var requestedUser = new User
            {
                UserId = userUpdateDTO.UserId,
                UserName = userUpdateDTO.Name,
                UserPhone = userUpdateDTO.PhoneNumber,
            };

            await _userRepository.UpdateUserAsync(requestedUser);

            return new UserResponseDTO
            {
                userEmail = requestedUser.UserEmail,
                userName =requestedUser.UserName,
                userPhone = requestedUser.UserPhone,
            };
        }

        public Task ResetUserPasswordAsync(int userId, string newPassword) => _userRepository.ResetPasswordAsync(userId, newPassword);

    }
}
