using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Application.Models;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;

namespace OnlineMedicineBookingApplication.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminContract _adminRepository;
        public AdminService(IAdminContract adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<Admin> AdminLoginAsync(AdminDTO admin)  // only login for admin
        {
            return await _adminRepository.GetAdminByEmailAndPassword(admin.AdminEmail, admin.AdminPassword);

        }
    }
}