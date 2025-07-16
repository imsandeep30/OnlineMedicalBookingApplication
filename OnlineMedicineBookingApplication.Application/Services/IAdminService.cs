using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Application.Models;

namespace OnlineMedicineBookingApplication.Application.Services
{
    public interface IAdminService
    {
        Task<Admin> AdminLoginAsync(AdminDTO admin);
    }
}
