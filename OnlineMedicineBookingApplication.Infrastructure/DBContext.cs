using Microsoft.EntityFrameworkCore;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.SeedingData;

namespace OnlineMedicineBookingApplication.Infrastructure
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Pharmacist> Pharmacists { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=IAM_MANIDEEP_\\SQLEXPRESS;Initial Catalog=OnlineMedicineBooking;Integrated Security=True;Encrypt=False";
            optionsBuilder.UseSqlServer(connectionString);
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
                }
            );

            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    AdminId = 1,
                    AdminName = "admin",
                    AdminPhone = "7093454577",
                    AdminEmail = "Admin@gmail.com",
                    AdminPassword = "Admin@123",
                }
            );
            modelBuilder.Entity<Pharmacist>().HasData(
                   new Pharmacist
                   {
                       PharmacistId = 1,
                       PharmacistName = "Pharmacist",
                       PharmacistPhone = "7093454577",
                       PharmacistEmail = "Pharamacist123@gmail.com",
                       PharmacistPassword = "Pharamacist@123",
                       PharmacyName = "Appolo",
                       Location = "Hyderabad",
                       IsApproved = true,
                   }    
            );
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
