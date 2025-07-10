using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineMedicineBookingApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class start : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AdminPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    MedicineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityAvailable = table.Column<int>(type: "int", nullable: false),
                    ManufactureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.MedicineId);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacists",
                columns: table => new
                {
                    PharmacistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacistName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PharmacistEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PharmacistPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PharmacistPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PharmacyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacists", x => x.PharmacistId);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    PrescriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.PrescriptionId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminId", "AdminEmail", "AdminName", "AdminPassword", "AdminPhone" },
                values: new object[] { 1, "Admin@gmail.com", "admin", "Admin@123", "7093454577" });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "MedicineId", "Brand", "Description", "ExpiryDate", "ManufactureDate", "MedicineName", "Price", "QuantityAvailable" },
                values: new object[,]
                {
                    { 1, "Crocin", "Used for fever and mild pain.", new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paracetamol", 20.00m, 100 },
                    { 2, "Mox", "Antibiotic for bacterial infections.", new DateTime(2027, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Amoxicillin", 35.50m, 80 },
                    { 3, "Cetzine", "Used to treat allergies and cold symptoms.", new DateTime(2026, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cetirizine", 15.75m, 200 },
                    { 4, "Brufen", "Pain reliever and anti-inflammatory.", new DateTime(2027, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ibuprofen", 22.00m, 150 },
                    { 5, "Rantac", "Reduces stomach acid, used for acidity.", new DateTime(2027, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ranitidine", 18.00m, 130 },
                    { 6, "Dolo", "Used for fever and body pain relief.", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dolo 650", 25.00m, 120 },
                    { 7, "Azithral", "Antibiotic used to treat bacterial infections.", new DateTime(2027, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Azithromycin", 55.00m, 90 },
                    { 8, "Electral", "Used to prevent dehydration.", new DateTime(2027, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ORS Powder", 10.00m, 300 },
                    { 9, "Pantocid", "Reduces stomach acid, used for ulcers and GERD.", new DateTime(2027, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pantoprazole", 30.00m, 110 },
                    { 10, "Asthalin", "Used for asthma and breathing issues.", new DateTime(2027, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Salbutamol Inhaler", 150.00m, 50 }
                });

            migrationBuilder.InsertData(
                table: "Pharmacists",
                columns: new[] { "PharmacistId", "IsApproved", "Location", "PharmacistEmail", "PharmacistName", "PharmacistPassword", "PharmacistPhone", "PharmacyName" },
                values: new object[] { 1, true, "Hyderabad", "Pharamacist123@gmail.com", "Pharmacist", "Pharamacist@123", "7093454577", "Appolo" });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "PrescriptionId", "FileName", "FilePath", "Status", "UserId" },
                values: new object[] { 1, "Prescription1.pdf", "/prescriptions/Prescription1.pdf", "Pending", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "UserEmail", "UserName", "UserPassword", "UserPhone" },
                values: new object[] { 1, "User@gmail.com", "user", "Test@123", "7093454577" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Pharmacists");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
