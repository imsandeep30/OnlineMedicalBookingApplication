using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMedicineBookingApplication.Application.Models
{
    public class MedicineFilterDTO
    {
        public string? SearchText { get; set; } = "";
        public decimal? MinPrice { get; set; } = null;
        public decimal? MaxPrice { get; set; } = null;
        public bool? OnlyAvailable { get; set; }
        public List<string>? ProblemKeywords { get; set; } = new List<string>();

    }

}
