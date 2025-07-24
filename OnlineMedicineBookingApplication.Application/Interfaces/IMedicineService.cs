using OnlineMedicineBookingApplication.Application.Models.MedicineDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Interfaces
{
    public interface IMedicineService
    {
        Task<AddMedicineDTO> AddMedicine(AddMedicineDTO medicine);
        Task<MedicineDTO> UpdateMedicine(MedicineDTO medicine);
        Task DeleteMedicine(string name);

        Task<List<MedicineDTO>> GetAllMedicinesAsync();
        Task<List<MedicineDTO>> FilterMedicinesAsync(MedicineFilterDTO filter);
        Task<MedicineDTO?> GetMedicineByIdAsync(int id);
    }

}
