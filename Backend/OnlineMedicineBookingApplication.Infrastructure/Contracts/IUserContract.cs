using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Domain.Entities;

namespace OnlineMedicineBookingApplication.Infrastructure.Contracts
{
    public interface IUserContract
    {
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);//login user
        Task AddUserAsync(User user); //register user
        Task<User> GetUserByIdAsync(int id);               // view userProfile
        Task<List<User>> GetAllUsersAsync();               // view all users
        Task DeleteUserAsync(int id);                      // delete user
        Task<User> UpdateUserAsync(User user);                   // update user
        Task ResetPasswordAsync(int userId, string oldPassword, string newPassword);  // reset user password

        Task<User> SearchMail(string mail);
    }
}
