using OnlineMedicineBookingApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Infrastructure.Contracts
{
    public interface IAdminContract
    {
        Task<Admin> GetAdminByEmailAndPassword(string email, string password); // login admin
    }
}
