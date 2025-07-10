using OnlineMedicineBookingApplication.Domain;
using OnlineMedicineBookingApplication.Infrastructure;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Services;
using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models;
public class Program
{
    public static void Main(string[] args)
    {
        //using (var context = new DBContext())
        //{
        //    var things = context.Admins.ToList();
        //    foreach (var admin in things)
        //    {
        //        Console.WriteLine($"AdminId: {admin.AdminId}, AdminName: {admin.AdminName}, AdminEmail: {admin.AdminEmail}");
        //    }
        //}
        var service = new MedicineService();

        //Console.WriteLine("All Medicines:");
        //var all = service.GetAllMedicines();
        //foreach (var med in all)
        //{
        //    Console.WriteLine($"{med.MedicineName} - ${med.Price}");
        //}
        var allMedicines = service.GetAllMedicinesAsync();
        foreach (var med in allMedicines.Result)
        {
            Console.WriteLine($"{med.MedicineName} - ${med.Price} - {med.Description}");
        }
        var searching = new MedicineFilterDTO
        {
            ProblemKeywords = new List<string> {"headache" , "fever"},
        };
        var filtered = service.FilterMedicinesAsync(searching);
        foreach (var med in filtered.Result)
        {
            Console.WriteLine($"{med.MedicineName} - ${med.Price}");
        }
        //Console.WriteLine("Filtered Medicines:");
        //var filtered = service.FilterMedicines(searching);
        //foreach (var med in filtered)
        //{
        //    Console.WriteLine($"{med.MedicineName} - ${med.Price}");
        //}
    }
}