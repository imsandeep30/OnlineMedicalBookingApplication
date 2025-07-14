using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineMedicineBookingApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newMigration7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pharmacists");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.AddColumn<bool>(
                name: "presecptionRequired",
                table: "Medicines",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "MedicineId",
                keyValue: 1,
                column: "presecptionRequired",
                value: false);

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "MedicineId",
                keyValue: 2,
                column: "presecptionRequired",
                value: false);

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "MedicineId",
                keyValue: 3,
                column: "presecptionRequired",
                value: false);

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "MedicineId",
                keyValue: 4,
                column: "presecptionRequired",
                value: false);

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "MedicineId",
                keyValue: 5,
                column: "presecptionRequired",
                value: false);

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "MedicineId",
                keyValue: 6,
                column: "presecptionRequired",
                value: false);

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "MedicineId",
                keyValue: 7,
                column: "presecptionRequired",
                value: false);

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "MedicineId",
                keyValue: 8,
                column: "presecptionRequired",
                value: false);

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "MedicineId",
                keyValue: 9,
                column: "presecptionRequired",
                value: false);

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "MedicineId",
                keyValue: 10,
                column: "presecptionRequired",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "presecptionRequired",
                table: "Medicines");

            migrationBuilder.CreateTable(
                name: "Pharmacists",
                columns: table => new
                {
                    PharmacistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PharmacistEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PharmacistName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PharmacistPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PharmacistPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PharmacyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
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
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.PrescriptionId);
                });

            migrationBuilder.InsertData(
                table: "Pharmacists",
                columns: new[] { "PharmacistId", "IsApproved", "Location", "PharmacistEmail", "PharmacistName", "PharmacistPassword", "PharmacistPhone", "PharmacyName" },
                values: new object[] { 1, true, "Hyderabad", "Pharamacist123@gmail.com", "Pharmacist", "Pharamacist@123", "7093454577", "Appolo" });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "PrescriptionId", "FileName", "FilePath", "Status", "UserId" },
                values: new object[] { 1, "Prescription1.pdf", "/prescriptions/Prescription1.pdf", "Pending", 1 });
        }
    }
}
