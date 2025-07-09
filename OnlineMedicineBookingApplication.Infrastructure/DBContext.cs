using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Globalization;
using OnlineMedicineBookingApplication.Domain.Entities;
namespace OnlineMedicineBookingApplication.Infrastructure
{
    public class DBContext : DbContext
    {
        public DbSet<User> users;
        public DbSet<Admin> admins;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source = IAM_MANIDEEP_\\SQLEXPRESS; Initial Catalog = OnlineMedicineBooking; Integrated Security = True; Encrypt = False";
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
                    UserEmail="User@gmail.com",
                    UserPassword="Test@123",
                }
             );
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    AdminId = 1,
                    AdminName = "admin",
                    AdminType = "admin",
                    AdminPhone = "7093454577",
                    AdminEmail = "Admin@gmail.com",
                    AdminPassword = "Admin@123",
                }
            );
        }

    }
    
}
