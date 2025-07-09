using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Domain.Entities
{
    public class Medicine
    {
        [Key]
        public int MedicineId { get; set; }

        [Required(ErrorMessage = "Medicine Name is required")]
        [StringLength(100)]
        public string MedicineName { get; set; }

        [Required(ErrorMessage = "Brand Name is required")]
        [StringLength(100)]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int QuantityAvailable { get; set; }

        [Required(ErrorMessage = "Manufacture date is required")]
        public DateTime ManufactureDate { get; set; }

        [Required(ErrorMessage = "Expiry date is required")]
        public DateTime ExpiryDate { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}
