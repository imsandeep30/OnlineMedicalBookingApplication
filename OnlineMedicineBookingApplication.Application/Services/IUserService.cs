using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Application.Models;
using OnlineMedicineBookingApplication.Domain.Entities;

namespace OnlineMedicineBookingApplication.Application.Services
{
    public interface IUserService
    {
        Task<User> LoginAsync(UserDTO user);

        Task RegisterAsync(User user);
        Task<User> GetUserProfile(int id);
        Task<List<User>> GetAllUsers();
        Task DeleteUser(int id);
        Task UpdateUser(User user);
        Task ResetUserPassword(int userId, string newPassword);

    }
}
