using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Application.Models.UserDTOS;
using OnlineMedicineBookingApplication.Domain.Entities;

namespace OnlineMedicineBookingApplication.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> LoginAsync(UserDTO user);

        Task RegisterAsync(UserRegisterDTO userRegisterDTO);
        Task<User> GetUserProfile(int id);
        Task<List<User>> GetAllUsers();
        Task DeleteUser(int id);
        Task UpdateUser(UserUpdateDTO user);
        Task ResetUserPassword(int userId, string newPassword);

    }
}
