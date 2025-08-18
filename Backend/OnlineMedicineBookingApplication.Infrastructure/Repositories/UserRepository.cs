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
            var res = await _context.Users.FirstOrDefaultAsync(u=>u.UserEmail==user.UserEmail);
            if (res == null)
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }
        }

        // Retrieves a user by their unique ID
        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.Address)   // Eagerly load the Address
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }
            return user;
        }

        // Gets all users from the Users table
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Address)  // Eager load Address for each user
                .ToListAsync();
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
            var existingUser = await _context.Users
                .Include(u => u.Address) // Include Address navigation property
                .FirstOrDefaultAsync(u => u.UserId == user.UserId);

            if (existingUser == null)
            {
                throw new ArgumentException("User not found.");
            }

            // Update user fields
            existingUser.UserName = user.UserName;
            existingUser.UserPhone = user.UserPhone;

            // Update address fields if provided
            if (user.Address != null)
            {
                if (existingUser.Address == null)
                {
                    // If no address exists, add new one
                    existingUser.Address = user.Address;
                }
                else
                {
                    existingUser.Address.Street = user.Address.Street;
                    existingUser.Address.City = user.Address.City;
                    existingUser.Address.State = user.Address.State;
                    existingUser.Address.ZipCode = user.Address.ZipCode;
                    existingUser.Address.Country = user.Address.Country;
                }
            }

            await _context.SaveChangesAsync();
            return existingUser;
        }


        // Resets the user's password
        public async Task ResetPasswordAsync(int userId, string oldPassword,string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                if(user.UserPassword != oldPassword)
                {
                    throw new ArgumentException("Old password is incorrect.");
                }
                else
                {
                    user.UserPassword = newPassword;
                }
                // In production, you should hash the password here
                await _context.SaveChangesAsync();
            }
        } 
        public async Task<User> SearchMail(string mail)
        {
            var user = await _context.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.UserEmail == mail);
            return user;
        }
    }
}
