using OnlineMedicineBookingApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OnlineMedicineBookingApplication.Domain.Entities
{
    public class UserWork
    {

        string[] serviceswork = {
         "add user","delete user", "view users", "view all users",
        " update user",
         "validate user login",
         "reset user password",
         "view cart call",
        "update cart call",
        "add to cart call",
        "remove from cart call",
        "view order status call"
        };
    }
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
        [MaxLength(50)]
        public string UserPassword { get; set; }
    }
}
