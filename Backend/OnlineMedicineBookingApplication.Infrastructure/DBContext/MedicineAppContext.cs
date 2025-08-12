using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using OnlineMedicineBookingApplication.Domain.Entities;
using OnlineMedicineBookingApplication.Infrastructure.SeedingData;
using System.Runtime.CompilerServices;

namespace OnlineMedicineBookingApplication.Infrastructure.DBContext
{
    public class MedicineAppContext : DbContext
    {
        // DbSets for each entity in the application
        public DbSet<User> Users { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
        //public DbSet<Pharmacist> Pharmacists { get; set; }
        //public DbSet<Prescription> Prescriptions { get; set; }

        // Constructor to pass options to the base DbContext
        public MedicineAppContext(DbContextOptions<MedicineAppContext> options)
        : base(options)
        {
        }
        //Seeding the data to the database tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the primary keys and relationships for each entity
            modelBuilder.Entity<User>()
                 .HasOne(u => u.Cart)
                 .WithOne(c => c.User)
                 .HasForeignKey<Cart>(c => c.UserId)
                 .IsRequired();
            modelBuilder.Entity<User>()
            .HasOne(u => u.Address)
            .WithOne(a => a.User)
            .HasForeignKey<Adress>(a => a.UserId)
            .IsRequired();

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Cart)
                .HasForeignKey(i => i.CartId);

            // 1:N User → Orders
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1:N Order → OrderItems
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1:1 Order → Transaction
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Order)
                .WithOne(o => o.Transaction)
                .HasForeignKey<Transaction>(t => t.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "user",
                    UserPhone = "7093454577",
                    UserEmail = "User@gmail.com",
                    UserPassword = "Test@123",  // Ideally, hashed
                    Role = "User",
                    CreatedAt = new DateTime(2025, 7, 12)
                },
                new User
                {
                    UserId = 2,
                    UserName = "admin",
                    UserPhone = "7093454577",
                    UserEmail = "admin@gmial.com",
                    UserPassword = "Admin@123",
                    Role = "Admin",
                    CreatedAt = new DateTime(2025, 7, 12)
                }
            );
            modelBuilder.Entity<Adress>().HasData(
                new Adress
                {
                    AdressId = 1,
                    Street = "123 Main St",
                    City = "Hyderabad",
                    State = "Telangana",
                    ZipCode = "500001",
                    Country = "India",
                    UserId = 1  // FK to the user
                },
                 new Adress
                 {
                     AdressId = 2,
                     Street = "345 Main St",
                     City = "Hyderabad",
                     State = "Telangana",
                     ZipCode = "500006",
                     Country = "India",
                     UserId = 2  // FK to the user
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
