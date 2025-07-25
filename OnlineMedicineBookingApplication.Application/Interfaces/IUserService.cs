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
        Task<User> GetUserProfileAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task DeleteUserAsync(int id);
        Task<UserResponseDTO> UpdateUserAsync(UserUpdateDTO user);
        Task ResetUserPasswordAsync(int userId, string newPassword);

    }
}
