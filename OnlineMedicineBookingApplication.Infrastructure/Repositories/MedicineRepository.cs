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

        // Constructor that injects the database context
        public MedicineRepository(MedicineAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Adds a new medicine record to the database
        public Task AddMedicine(Medicine medicine)
        {
            _context.Medicines.Add(medicine);
            return _context.SaveChangesAsync();
        }

        // Deletes a medicine by its ID
        public Task DeleteMedicine(int medicineId)
        {
            var medicine = _context.Medicines.FirstOrDefault(m => m.MedicineId == medicineId);
            if (medicine != null)
            {
                _context.Medicines.Remove(medicine);
                return _context.SaveChangesAsync();
            }
            return Task.CompletedTask; // Nothing to delete
        }

        // Retrieves all medicines from the database
        public async Task<List<Medicine>> GetAllAsync()
        {
            return await _context.Medicines.ToListAsync();
        }

        // Gets a specific medicine by ID
        public async Task<Medicine?> GetByIdAsync(int id)
        {
            return await _context.Medicines.FirstOrDefaultAsync(m => m.MedicineId == id);
        }

        // Filters medicines based on optional search text, price range, availability, and keywords
        public async Task<List<Medicine>> FilterAsync(string? searchText, decimal? min, decimal? max, bool? onlyAvailable, List<string>? keywords)
        {
            var query = _context.Medicines.AsQueryable(); // Start with full list

            // Filter by name or brand if search text is provided
            if (!string.IsNullOrEmpty(searchText))
                query = query.Where(m => m.MedicineName.Contains(searchText) || m.Brand.Contains(searchText));

            // Filter by minimum price
            if (min.HasValue)
                query = query.Where(m => m.Price >= min);

            // Filter by maximum price
            if (max.HasValue)
                query = query.Where(m => m.Price <= max);

            // Filter only available stock
            if (onlyAvailable == true)
                query = query.Where(m => m.QuantityAvailable > 0);

            // Filter by keywords in the description
            if (keywords != null && keywords.Any())
                query = query.Where(m => keywords.Any(k => m.Description.ToLower().Contains(k.ToLower())));

            return await query.ToListAsync(); // Execute the final query
        }
    }
}
