using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Domain.Entities;
namespace OnlineMedicineBookingApplication.Infrastructure.Contracts
{
    public interface IMedicineContract
    {
        Task<List<Medicine>> GetAllAsync();
        Task<List<Medicine>> FilterAsync(string? searchText, decimal? min, decimal? max, bool? onlyAvailable, List<string>? keywords);
        Task<Medicine?> GetByIdAsync(int id);
        Task AddMedicine(Medicine medicine);

        Task UpdateMedicine(Medicine medicine);
        Task DeleteMedicine(int medicineId);
    }
}
