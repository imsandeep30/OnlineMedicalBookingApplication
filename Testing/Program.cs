using OnlineMedicineBookingApplication.Domain;
using OnlineMedicineBookingApplication.Infrastructure;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
public class Program
{
    public static void Main(string[] args)
    {
        using (var context = new DBContext())
        {
            var things = context.Admins.ToList();
            foreach (var admin in things)
            {
                Console.WriteLine($"AdminId: {admin.AdminId}, AdminName: {admin.AdminName}, AdminEmail: {admin.AdminEmail}");
            }
        }
    }
}