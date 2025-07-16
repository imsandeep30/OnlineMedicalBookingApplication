using OnlineMedicineBookingApplication.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;

namespace OnlineMedicineBookingApplication.Infrastructure.Repositories
{
    public class AdminRepository : IAdminContract
    {
        private readonly MedicineAppContext _context;

        public AdminRepository()
        {
            _context = new MedicineAppContext();
        }
        public async Task<Admin> GetAdminByEmailAndPassword(string email, string password)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.AdminEmail == email && a.AdminPassword == password);
        }
    }
}
