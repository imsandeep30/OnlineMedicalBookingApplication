using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;
using OnlineMedicineBookingApplication.Infrastructure.DBContext;
using OnlineMedicineBookingApplication.Application.Models;
namespace OnlineMedicineBookingApplication.Application.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineContract _medicineRepository;

        public MedicineService(IMedicineContract medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }
        public async Task AddMedicine(MedicineDTO medicineDto)
        {
            var medicine = new Medicine
            {
                MedicineName = medicineDto.MedicineName,
                Brand = medicineDto.Brand,
                Price = medicineDto.Price,
                QuantityAvailable = medicineDto.QuantityAvailable,
                Description = medicineDto.Description
            };
            await _medicineRepository.AddMedicine(medicine);
        }
        public async Task DeleteMedicine(string name)
        {
            var medicine = await _medicineRepository.FilterAsync(name, null, null, null, null);
            if (medicine == null || medicine.Count == 0)
                throw new KeyNotFoundException("Medicine not found.");
            else
            {
                foreach (var med in medicine)
                {
                    await _medicineRepository.DeleteMedicine(med.MedicineId);
                }
            }
        }
        public async Task UpdateMedicine(MedicineDTO medicine)
        {
            var existingMedicine = await _medicineRepository.GetByIdAsync(medicine.MedicineId);
            if (existingMedicine == null)
                throw new KeyNotFoundException("Medicine not found.");
            else
            {
                existingMedicine.MedicineName = medicine.MedicineName;
                existingMedicine.Brand = medicine.Brand;
                existingMedicine.Price = medicine.Price;
                existingMedicine.QuantityAvailable = medicine.QuantityAvailable;
                existingMedicine.Description = medicine.Description;
                await _medicineRepository.DeleteMedicine(existingMedicine.MedicineId);
                await _medicineRepository.AddMedicine(existingMedicine);
            }
        }
        public async Task<List<MedicineDTO>> GetAllMedicinesAsync()
        {
            var medicines = await _medicineRepository.GetAllAsync();
            return medicines.Select(MapToDTO).ToList();
        }

        public async Task<List<MedicineDTO>> FilterMedicinesAsync(MedicineFilterDTO filter)
        {
            var medicines = await _medicineRepository.FilterAsync(
                filter.SearchText,
                filter.MinPrice,
                filter.MaxPrice,
                filter.OnlyAvailable,
                filter.ProblemKeywords
            );

            return medicines.Select(MapToDTO).ToList();
        }

        public async Task<MedicineDTO?> GetMedicineByIdAsync(int id)
        {
            var med = await _medicineRepository.GetByIdAsync(id);
            return med != null ? MapToDTO(med) : null;
        }

        private MedicineDTO MapToDTO(Medicine m) => new MedicineDTO
        {
            MedicineId = m.MedicineId,
            MedicineName = m.MedicineName,
            Brand = m.Brand,
            Price = m.Price,
            QuantityAvailable = m.QuantityAvailable,
            Description = m.Description,
        };
    }

}
