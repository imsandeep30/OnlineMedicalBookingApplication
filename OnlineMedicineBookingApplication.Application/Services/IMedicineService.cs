using OnlineMedicineBookingApplication.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Services
{
    public interface IMedicineService
    {
        Task AddMedicine(MedicineDTO medicine);
        Task UpdateMedicine(MedicineDTO medicine);
        Task DeleteMedicine(string name);

        Task<List<MedicineDTO>> GetAllMedicinesAsync();
        Task<List<MedicineDTO>> FilterMedicinesAsync(MedicineFilterDTO filter);
        Task<MedicineDTO?> GetMedicineByIdAsync(int id);
    }

}
