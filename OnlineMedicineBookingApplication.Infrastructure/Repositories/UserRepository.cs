using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;
using OnlineMedicineBookingApplication.Infrastructure.DBContext;

namespace OnlineMedicineBookingApplication.Infrastructure.Repositories
{
    public class UserRepository: IUserContract
    {
        private readonly MedicineAppContext _context;

        public UserRepository(MedicineAppContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == email && u.UserPassword == password);
        }
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }
            return user;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserId);
            if(existingUser == null)
            {
                   throw new ArgumentException("User not found.");
            }
            else
            {
                existingUser.UserName = user.UserName;
                existingUser.UserPhone = user.UserPhone;
                await _context.SaveChangesAsync();
                return existingUser;
            }
        }

        public async Task ResetPasswordAsync(int userId, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.UserPassword = newPassword; //Should hash in production
                await _context.SaveChangesAsync();
            }
        }

    }
}
