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

        public UserRepository()
        {
            _context = new MedicineAppContext();
        }

        public async Task<User> GetUserByEmailAndPassword(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == email && u.UserPassword == password);
        }
        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task ResetPassword(int userId, string newPassword)
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
