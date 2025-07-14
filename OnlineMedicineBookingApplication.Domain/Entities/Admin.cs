using System.ComponentModel.DataAnnotations;

namespace OnlineMedicineBookingApplication.Domain.Entities
{
    public class workneed
    {
        string[] services_work = {"admin login",
         "add medicine call",
         "delete medicine call",
         "update medicine call",
         "view medicine call",
         "add user call",
         "delete user call",
        "update order status call",
        "updatae order validate call"};
    }

public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required(ErrorMessage = "Admin Name is required")]
        [StringLength(100, ErrorMessage = "Admin Name cannot be longer than 100 characters.")]
        public string AdminName { get; set; }
        [Required(ErrorMessage = "Admin Phone is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Admin Phone must be a 10-digit number.")]
        public string AdminPhone { get; set; }
        [Required(ErrorMessage = "Admin Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string AdminEmail { get; set; }
        [Required(ErrorMessage = "User Password is required")]
        [MaxLength(50)]
        public string AdminPassword { get; set; }

    }
    
}