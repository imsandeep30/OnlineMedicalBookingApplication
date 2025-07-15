using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Application.Models;
using OnlineMedicineBookingApplication.Domain.Entities;

namespace OnlineMedicineBookingApplication.Application.Services
{
    public interface IAuthService
    {
        Task<User> LoginAsync(UserDTO user);

        Task RegisterAsync(User user);
    }
}
