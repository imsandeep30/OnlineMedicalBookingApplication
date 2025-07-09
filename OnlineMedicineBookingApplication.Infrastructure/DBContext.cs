using Microsoft.EntityFrameworkCore;
using OnlineMedicineBookingApplication.Domain.Entities;

namespace OnlineMedicineBookingApplication.Infrastructure
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }

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
                    AdminType = "admin",
                    AdminPhone = "7093454577",
                    AdminEmail = "Admin@gmail.com",
                    AdminPassword = "Admin@123",
                }
            );
        }
    }
}
