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
    }
}
