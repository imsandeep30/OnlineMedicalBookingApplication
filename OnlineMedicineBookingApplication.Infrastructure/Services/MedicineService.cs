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

        public List<MedicineDTO> GetAllMedicines()
        {
            return _context.Medicines.Select(MapToDTO).ToList();
        }

        public List<MedicineDTO> FilterMedicines(MedicineFilterDTO filter)
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

            return query.Select(MapToDTO).ToList();
        }

        public MedicineDTO GetMedicineById(int id)
        {
            var med = _context.Medicines.FirstOrDefault(m => m.MedicineId == id);
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
