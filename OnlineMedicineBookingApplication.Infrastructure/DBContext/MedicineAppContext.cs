using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.SeedingData;
using System.Runtime.CompilerServices;

namespace OnlineMedicineBookingApplication.Infrastructure.DBContext
{
    public class MedicineAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        //public DbSet<Pharmacist> Pharmacists { get; set; }
        //public DbSet<Prescription> Prescriptions { get; set; }

        public MedicineAppContext(DbContextOptions<MedicineAppContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "user",
                    UserPhone = "7093454577",
                    UserEmail = "User@gmail.com",
                    UserPassword = "Test@123",
                    Role = "User",
                },
                new User
                {
                    UserId = 2,
                    UserName = "admin",
                    UserPhone = "7093454577",
                    UserEmail = "admin@gmial.com",
                    UserPassword = "Admin@123",
                    Role = "Admin",
                }
            );

            //modelBuilder.Entity<Pharmacist>().HasData(
            //       new Pharmacist
            //       {
            //           PharmacistId = 1,
            //           PharmacistName = "Pharmacist",
            //           PharmacistPhone = "7093454577",
            //           PharmacistEmail = "Pharamacist123@gmail.com",
            //           PharmacistPassword = "Pharamacist@123",
            //           PharmacyName = "Appolo",
            //           Location = "Hyderabad",
            //           IsApproved = true,
            //       }
            //);
            //modelBuilder.Entity<Prescription>().HasData(
            //    new Prescription
            //    {
            //        PrescriptionId = 1,
            //        UserId = 1,
            //        FileName = "Prescription1.pdf",
            //        FilePath = "/prescriptions/Prescription1.pdf",
            //        Status = "Pending"
            //    }
            //);
            var medicines = MedicineSeedData.GetMedicine();
            int id = 1;

            foreach (var med in medicines)
            {
                med.MedicineId = id++;
                modelBuilder.Entity<Medicine>().HasData(med);
            }
        }
    }
}
