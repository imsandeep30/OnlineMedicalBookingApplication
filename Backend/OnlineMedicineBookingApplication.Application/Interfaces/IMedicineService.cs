using OnlineMedicineBookingApplication.Application.Models.MedicineDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Interfaces
{
    // Interface for medicine-related business operations
    public interface IMedicineService
    {
        // Adds a new medicine to the catalog
        Task<AddMedicineDTO> AddMedicine(AddMedicineDTO medicine);

        // Updates an existing medicine's details
        Task<MedicineDTO> UpdateMedicine(MedicineDTO medicine);

        // Deletes a medicine from the catalog based on its Id
        Task DeleteMedicine(int medicineId);

        // Retrieves all medicines from the system
        Task<List<MedicineDTO>> GetAllMedicinesAsync();

        // Filters medicines based on criteria such as category, price range, etc.
        Task<List<MedicineDTO>> FilterMedicinesAsync(MedicineFilterDTO filter);

        // Gets the details of a medicine by its unique ID
        Task<MedicineDTO?> GetMedicineByIdAsync(int id);
    }
}
