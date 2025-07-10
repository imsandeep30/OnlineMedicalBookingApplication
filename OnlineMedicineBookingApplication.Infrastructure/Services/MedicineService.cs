using Microsoft.EntityFrameworkCore;
using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models;
using OnlineMedicineBookingApplication.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OnlineMedicineBookingApplication.Infrastructure.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly DBContext _context;

        public MedicineService()
        {
            _context = new DBContext();
        }

        public async Task<List<MedicineDTO>> GetAllMedicinesAsync()
        {
            var medicines = await _context.Medicines.ToListAsync();
            return medicines.Select(MapToDTO).ToList();
        }

        public async Task<List<MedicineDTO>> FilterMedicinesAsync(MedicineFilterDTO filter)
        {
            var query = _context.Medicines.AsQueryable();

            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                query = query.Where(m =>
                    m.MedicineName.Contains(filter.SearchText) ||
                    m.Brand.Contains(filter.SearchText));
            }

            if (filter.MinPrice.HasValue)
                query = query.Where(m => m.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(m => m.Price <= filter.MaxPrice.Value);

            if (filter.OnlyAvailable == true)
                query = query.Where(m => m.QuantityAvailable > 0);

            if (filter.ProblemKeywords != null && filter.ProblemKeywords.Any())
            {
                query = query.Where(m =>filter.ProblemKeywords.Any(kw =>m.Description.ToLower().Contains(kw.ToLower())));
            }

            var medicines = await query.ToListAsync();
            return medicines.Select(MapToDTO).ToList();
        }

        public async Task<MedicineDTO?> GetMedicineByIdAsync(int id)
        {
            var med = await _context.Medicines.FirstOrDefaultAsync(m => m.MedicineId == id);
            return med != null ? MapToDTO(med) : null;
        }

        private MedicineDTO MapToDTO(Medicine m) => new MedicineDTO
        {
            MedicineId = m.MedicineId,
            MedicineName = m.MedicineName,
            Brand = m.Brand,
            Price = m.Price,
            QuantityAvailable = m.QuantityAvailable,
            Description = m.Description
        };
    }
}
