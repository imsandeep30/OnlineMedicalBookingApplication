using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Domain.Entities
{
        public class Pharmacist
        {
            [Key]
            public int PharmacistId { get; set; }

            [Required(ErrorMessage = "Pharmacist Name is required")]
            [StringLength(100)]
            public string PharmacistName { get; set; }

            [Required(ErrorMessage = "Pharmacist Email is required")]
            [EmailAddress(ErrorMessage = "Invalid Email Address")]
            public string PharmacistEmail { get; set; }

            [Required(ErrorMessage = "Pharmacist Phone is required")]
            [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone must be a 10-digit number")]
            public string PharmacistPhone { get; set; }

            [Required(ErrorMessage = "Pharmacist Password is required")]
            [MaxLength(50)]
            public string PharmacistPassword { get; set; }

            [Required(ErrorMessage = "Pharmacy Name is required")]
            [StringLength(100)]
            public string PharmacyName { get; set; }

            [Required(ErrorMessage = "Location is required")]
            [StringLength(200)]
            public string Location { get; set; }

            public bool IsApproved { get; set; } = false; // default status before admin approval
        }
}
