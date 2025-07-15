using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Domain.Entities;

namespace OnlineMedicineBookingApplication.Infrastructure.Contracts
{
    public interface IUserContract
    {
        Task<User> GetUserByEmailAndPassword(string email, string password);
        Task AddUser(User user);
    }
}
