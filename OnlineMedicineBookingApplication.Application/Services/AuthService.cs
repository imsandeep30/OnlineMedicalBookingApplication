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
    public class AuthService: IAuthService
    {
        private readonly IUserContract _userRepository;

        public AuthService(IUserContract userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> LoginAsync(UserDTO user)
        {
            return await _userRepository.GetUserByEmailAndPassword(user.UserEmail, user.UserPassword);
        }

        public async Task RegisterAsync(User user)
        {
            await _userRepository.AddUser(user);
        }
    }
}
