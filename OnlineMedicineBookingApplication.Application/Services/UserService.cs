using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models;
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


        public UserService(IUserContract userRepository)
        {
            _userRepository = userRepository;
       
        }

        public async Task<User> LoginAsync(UserDTO user)
        {
            return await _userRepository.GetUserByEmailAndPassword(user.UserEmail, user.UserPassword);
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
        }
        public Task<User> GetUserProfile(int id) => _userRepository.GetUserById(id);

        public Task<List<User>> GetAllUsers() => _userRepository.GetAllUsers();

        public Task DeleteUser(int id) => _userRepository.DeleteUser(id);

        public Task UpdateUser(UserUpdateDTO userUpdateDTO)
        {
            var user = new User
            {
                UserName = userUpdateDTO.Name,
                UserEmail = userUpdateDTO.Email,
                UserPhone = userUpdateDTO.PhoneNumber,
                UserPassword = userUpdateDTO.Password,
                Role = userUpdateDTO.Role,
            };
            return _userRepository.UpdateUser(user);
        }

        public Task ResetUserPassword(int userId, string newPassword) => _userRepository.ResetPassword(userId, newPassword);

    }
}
