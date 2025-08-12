using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Models.UserDTOS
{
    public class AdressDTO
    {
        public string UserStreet { get; set; }
        public string UserCity { get; set; }
        public string UserState { get; set; }
        public string UserCountry { get; set; }
        public string UserZipCode { get; set; }
    }
}
