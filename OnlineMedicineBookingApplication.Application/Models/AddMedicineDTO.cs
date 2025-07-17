using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Models
{
    public class AddMedicineDTO
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Description { get; set; }
        public bool presecptionRequired { get; set; } = false;

    }
}
