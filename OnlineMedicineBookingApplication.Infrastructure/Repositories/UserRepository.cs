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
    public class UserRepository : IUserContract
    {
        private readonly MedicineAppContext _context;

        // Constructor injecting the DB context
        public UserRepository(MedicineAppContext context)
        {
            _context = context;
        }

        // Retrieves a user by matching email and password
        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == email && u.UserPassword == password);
        }

        // Adds a new user to the database
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        // Retrieves a user by their unique ID
        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }
            return user;
        }

        // Gets all users from the Users table
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Deletes a user by ID if they exist
        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // Updates user fields (name and phone) if user exists
        public async Task<User> UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser == null)
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

        // Resets the user's password
        public async Task ResetPasswordAsync(int userId, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                // In production, you should hash the password here
                user.UserPassword = newPassword;
                await _context.SaveChangesAsync();
            }
        }
    }
}
