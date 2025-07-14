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
    public class MedicineRepository : IMedicineContract
    {
        private readonly MedicineAppContext _context;

        public MedicineRepository()
        {
            _context = new MedicineAppContext();
        }
        public Task AddMedicine(Medicine medicine)
        {
            _context.Medicines.Add(medicine);
            return _context.SaveChangesAsync();
        }
        public Task DeleteMedicine(int medicineId)
        {
            var medicine = _context.Medicines.FirstOrDefault(m => m.MedicineId == medicineId);
            if (medicine != null)
            {
                _context.Medicines.Remove(medicine);
                return _context.SaveChangesAsync();
            }
            return Task.CompletedTask;
        }

        public async Task<List<Medicine>> GetAllAsync()
        {
            return await _context.Medicines.ToListAsync();
        }

        public async Task<Medicine?> GetByIdAsync(int id)
        {
            return await _context.Medicines.FirstOrDefaultAsync(m => m.MedicineId == id);
        }

        public async Task<List<Medicine>> FilterAsync(string? searchText, decimal? min, decimal? max, bool? onlyAvailable, List<string>? keywords)
        {
            var query = _context.Medicines.AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
                query = query.Where(m => m.MedicineName.Contains(searchText) || m.Brand.Contains(searchText));

            if (min.HasValue)
                query = query.Where(m => m.Price >= min);

            if (max.HasValue)
                query = query.Where(m => m.Price <= max);

            if (onlyAvailable == true)
                query = query.Where(m => m.QuantityAvailable > 0);

            if (keywords != null && keywords.Any())
                query = query.Where(m => keywords.Any(k => m.Description.ToLower().Contains(k.ToLower())));

            return await query.ToListAsync();
        }
    }

}
