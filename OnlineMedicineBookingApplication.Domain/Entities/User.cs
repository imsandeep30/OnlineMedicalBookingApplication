using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OnlineMedicineBookingApplication.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        [StringLength(100, ErrorMessage = "User Name cannot be longer than 100 characters.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "User Phone is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "User Phone must be a 10-digit number.")]
        public string UserPhone { get; set; }
        [Required(ErrorMessage = "User Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "User Password is required")]
        [StringLength(6, ErrorMessage = "User Password must be exactly 6 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
    }
}
