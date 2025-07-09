using System.ComponentModel.DataAnnotations;

namespace OnlineMedicineBookingApplication.Domain.Entities
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required(ErrorMessage = "Admin Name is required")]
        [StringLength(100, ErrorMessage = "Admin Name cannot be longer than 100 characters.")]
        public string AdminName { get; set; }
        [Required(ErrorMessage = "Admin Type is required")]
        [StringLength(50, ErrorMessage = "Admin Type cannot be longer than 50 characters.")]
        public string AdminType { get; set; }
        [Required(ErrorMessage = "Admin Phone is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Admin Phone must be a 10-digit number.")]
        public string AdminPhone { get; set; }
        [Required(ErrorMessage = "Admin Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string AdminEmail { get; set; }
        [Required(ErrorMessage = "Admin Password is required")]
        [StringLength(6, ErrorMessage = "Admin Password must be exactly 6 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string AdminPassword { get; set; }


    }
}
