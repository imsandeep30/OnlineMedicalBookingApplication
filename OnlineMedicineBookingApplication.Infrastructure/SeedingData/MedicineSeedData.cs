using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineMedicineBookingApplication.Domain.Entities;

namespace OnlineMedicineBookingApplication.Infrastructure.SeedingData
{
    public class MedicineSeedData
    {
        public static List<Medicine> GetMedicine()
        {
            return new List<Medicine>()
            {
                new Medicine
                {
                    MedicineId = 1,
                    MedicineName = "Paracetamol",
                    Brand = "Crocin",
                    Price = 20.00m,
                    QuantityAvailable = 100,
                    ManufactureDate = new DateTime(2024, 12, 1),
                    ExpiryDate = new DateTime(2026, 12, 1),
                    Description = "Used for fever and mild pain.",
                },
                new Medicine
                {
                    MedicineId = 2,
                    MedicineName = "Amoxicillin",
                    Brand = "Mox",
                    Price = 35.50m,
                    QuantityAvailable = 80,
                    ManufactureDate = new DateTime(2025, 1, 10),
                    ExpiryDate = new DateTime(2027, 1, 10),
                    Description = "Antibiotic for bacterial infections.",
                },
                new Medicine
                {
                    MedicineId = 3,
                    MedicineName = "Cetirizine",
                    Brand = "Cetzine",
                    Price = 15.75m,
                    QuantityAvailable = 200,
                    ManufactureDate = new DateTime(2024, 11, 5),
                    ExpiryDate = new DateTime(2026, 11, 5),
                    Description = "Used to treat allergies and cold symptoms.",
                },
                new Medicine
                {
                    MedicineId = 4,
                    MedicineName = "Ibuprofen",
                    Brand = "Brufen",
                    Price = 22.00m,
                    QuantityAvailable = 150,
                    ManufactureDate = new DateTime(2025, 2, 15),
                    ExpiryDate = new DateTime(2027, 2, 15),
                    Description = "Pain reliever and anti-inflammatory.",
                },
                new Medicine
                {
                    MedicineId = 5,
                    MedicineName = "Ranitidine",
                    Brand = "Rantac",
                    Price = 18.00m,
                    QuantityAvailable = 130,
                    ManufactureDate = new DateTime(2025, 3, 1),
                    ExpiryDate = new DateTime(2027, 3, 1),
                    Description = "Reduces stomach acid, used for acidity.",
                },
                new Medicine
                {
                    MedicineId = 6,
                    MedicineName = "Dolo 650",
                    Brand = "Dolo",
                    Price = 25.00m,
                    QuantityAvailable = 120,
                    ManufactureDate = new DateTime(2024, 10, 1),
                    ExpiryDate = new DateTime(2026, 10, 1),
                    Description = "Used for fever and body pain relief.",
                },
                new Medicine
                {
                    MedicineId = 7,
                    MedicineName = "Azithromycin",
                    Brand = "Azithral",
                    Price = 55.00m,
                    QuantityAvailable = 90,
                    ManufactureDate = new DateTime(2025, 4, 1),
                    ExpiryDate = new DateTime(2027, 4, 1),
                    Description = "Antibiotic used to treat bacterial infections.",
                },
                new Medicine
                {
                    MedicineId = 8,
                    MedicineName = "ORS Powder",
                    Brand = "Electral",
                    Price = 10.00m,
                    QuantityAvailable = 300,
                    ManufactureDate = new DateTime(2025, 5, 1),
                    ExpiryDate = new DateTime(2027, 5, 1),
                    Description = "Used to prevent dehydration.",
                },
                new Medicine
                {
                    MedicineId = 9,
                    MedicineName = "Pantoprazole",
                    Brand = "Pantocid",
                    Price = 30.00m,
                    QuantityAvailable = 110,
                    ManufactureDate = new DateTime(2025, 6, 1),
                    ExpiryDate = new DateTime(2027, 6, 1),
                    Description = "Reduces stomach acid, used for ulcers and GERD.",
                },
                new Medicine
                {
                    MedicineId = 10,
                    MedicineName = "Salbutamol Inhaler",
                    Brand = "Asthalin",
                    Price = 150.00m,
                    QuantityAvailable = 50,
                    ManufactureDate = new DateTime(2025, 7, 1),
                    ExpiryDate = new DateTime(2027, 7, 1),
                    Description = "Used for asthma and breathing issues.",
                }
            };
        }
    }
}
