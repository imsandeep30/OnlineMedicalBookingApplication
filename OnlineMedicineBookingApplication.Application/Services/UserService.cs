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
        private readonly ICartRepository _cartRepository;


        public UserService(IUserContract userRepository, ICartRepository cartRepository)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }

        public async Task<User> LoginAsync(UserDTO user)
        {
            return await _userRepository.GetUserByEmailAndPassword(user.UserEmail, user.UserPassword);
        }

        public async Task RegisterAsync(User user)
        {
            await _userRepository.AddUser(user);
        }
        public Task<User> GetUserProfile(int id) => _userRepository.GetUserById(id);

        public Task<List<User>> GetAllUsers() => _userRepository.GetAllUsers();

        public Task DeleteUser(int id) => _userRepository.DeleteUser(id);

        public Task UpdateUser(User user) => _userRepository.UpdateUser(user);

        public Task ResetUserPassword(int userId, string newPassword) => _userRepository.ResetPassword(userId, newPassword);

        public Task<Cart> GetUserCart(int userId) => _cartRepository.GetCartByUserIdAsync(userId);



    }
}
