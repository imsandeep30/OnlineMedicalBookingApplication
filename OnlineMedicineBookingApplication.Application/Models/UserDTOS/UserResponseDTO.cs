using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Models.UserDTOS
{
    public class UserResponseDTO
    {
        public string? userName { get; set; }
        public string? userPhone { get; set; }
        public string? userEmail { get; set; }
    }
}
