﻿using OnlineMedicineBookingApplication.Application.Services;
using OnlineMedicineBookingApplication.Infrastructure.Repositories;
using OnlineMedicineBookingApplication.Application.Models;

class Program
{
    static async Task Main(string[] args)
    {
        var repository = new MedicineRepository();
        var service = new MedicineService(repository);
        //var allMedicines = await service.GetAllMedicinesAsync();
        //foreach (var med in allMedicines)
        //{
        //    Console.WriteLine($"{med.MedicineName} - {med.Price} Rs");
        //}
        //var filter = new MedicineFilterDTO
        //{
        //    SearchText = "Para",
        //    MinPrice = 20,
        //    MaxPrice = 60,
        //    OnlyAvailable = true
        //};

        //var filtered = await service.FilterMedicinesAsync(filter);
        //Console.WriteLine("\nFiltered Medicines:");
        //foreach (var med in filtered)
        //{
        //    Console.WriteLine($"{med.MedicineName} - {med.Price} Rs");
        //}
        //var medDetails = await service.GetMedicineByIdAsync(1);
        //if (medDetails != null)
        //{
        //    Console.WriteLine($"\nDetails:\nName: {medDetails.MedicineName}\nDesc: {medDetails.Description}");
        //}
        var medicine = new MedicineDTO
        {
            MedicineName = "Paracetamol",
            Brand = "Generic",
            Price = 50,
            QuantityAvailable = 100,
            Description = "Pain reliever and fever reducer."
        };
        await service.DeleteMedicine("Paracetamol");
        var allMedicines = await service.GetAllMedicinesAsync();
        Console.WriteLine("\nAll Medicines After Adding New One:");
        foreach (var med in allMedicines)
        {
            Console.WriteLine($"{med.MedicineName} - {med.Price} Rs");
        }
    }
}
