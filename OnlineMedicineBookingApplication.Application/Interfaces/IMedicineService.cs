using OnlineMedicineBookingApplication.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Interfaces
{
    public interface IMedicineService
    {
        List<MedicineDTO> GetAllMedicines();
        List<MedicineDTO> FilterMedicines(MedicineFilterDTO filter);
        MedicineDTO GetMedicineById(int id);
    }
}
