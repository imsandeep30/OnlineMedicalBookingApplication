using OnlineMedicineBookingApplication.Domain;
using OnlineMedicineBookingApplication.Infrastructure;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.Services;
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

        Console.WriteLine("All Medicines:");
        var all = service.GetAllMedicines();
        foreach (var med in all)
        {
            Console.WriteLine($"{med.MedicineName} - ${med.Price}");
        }
    }
}