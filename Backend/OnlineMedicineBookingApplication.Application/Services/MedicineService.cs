using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models.MedicineDTOS;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Contracts;

public class MedicineService : IMedicineService
{
    private readonly IMedicineContract _medicineRepository;

    public MedicineService(IMedicineContract medicineRepository)
    {
        _medicineRepository = medicineRepository;
    }

    // Adds a new medicine record to the database
    public async Task<AddMedicineDTO> AddMedicine(AddMedicineDTO medicineDto)
    {
        var medicine = new Medicine
        {
            MedicineName = medicineDto.MedicineName,
            Brand = medicineDto.Brand,
            Price = medicineDto.Price,
            QuantityAvailable = medicineDto.QuantityAvailable,
            Description = medicineDto.Description,
            ManufactureDate = medicineDto.ManufactureDate,
            ExpiryDate = medicineDto.ExpiryDate,
            presecptionRequired = medicineDto.presecptionRequired,
        };
        await _medicineRepository.AddMedicine(medicine);
        return medicineDto;
    }

    // Deletes all medicines that match the given name
    public async Task DeleteMedicine(int medicineId)
    {
        var medicine = await _medicineRepository.GetByIdAsync(medicineId);
        if (medicine == null)
            throw new KeyNotFoundException("Medicine not found.");
        else
        { 
                await _medicineRepository.DeleteMedicine(medicineId);
        }
    }

    // Updates an existing medicine's details
    public async Task<MedicineDTO> UpdateMedicine(MedicineDTO medicine)
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

            await _medicineRepository.UpdateMedicine(existingMedicine);
            return medicine;
        }
    }

    // Retrieves all medicines in the database
    public async Task<List<MedicineDTO>> GetAllMedicinesAsync()
    {
        var medicines = await _medicineRepository.GetAllAsync();
        return medicines.Select(MapToDTO).ToList();
    }

    // Filters medicines based on search, price, availability, and problem keywords
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

    // Retrieves a specific medicine by its ID
    public async Task<MedicineDTO?> GetMedicineByIdAsync(int id)
    {
        var med = await _medicineRepository.GetByIdAsync(id);
        return med != null ? MapToDTO(med) : null;
    }

    // Maps a Medicine entity to a DTO for external use
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
