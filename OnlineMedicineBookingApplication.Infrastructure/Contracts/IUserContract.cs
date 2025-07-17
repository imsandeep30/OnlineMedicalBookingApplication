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
        Task<User> GetUserByEmailAndPassword(string email, string password);//login user
        Task AddUser(User user); //register user
        Task<User> GetUserById(int id);               // view userProfile
        Task<List<User>> GetAllUsers();               // view all users
        Task DeleteUser(int id);                      // delete user
        Task UpdateUser(User user);                   // update user
        Task ResetPassword(int userId, string newPassword);  // reset user password
    }
}
