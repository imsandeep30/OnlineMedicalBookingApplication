using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineMedicineBookingApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminType",
                table: "Admins");

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

            migrationBuilder.InsertData(
                table: "Pharmacists",
                columns: new[] { "PharmacistId", "IsApproved", "Location", "PharmacistEmail", "PharmacistName", "PharmacistPassword", "PharmacistPhone", "PharmacyName" },
                values: new object[] { 1, true, "Hyderabad", "Pharamacist123@gmail.com", "Pharmacist", "Pharamacist@123", "7093454577", "Appolo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pharmacists");

            migrationBuilder.AddColumn<string>(
                name: "AdminType",
                table: "Admins",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "AdminType",
                value: "admin");
        }
    }
}
