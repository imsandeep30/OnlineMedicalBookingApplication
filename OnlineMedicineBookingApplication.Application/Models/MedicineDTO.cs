using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Models
{
    public class MedicineDTO
    {
        public int MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public string? Brand { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public string? Description { get; set; }
    }

}
