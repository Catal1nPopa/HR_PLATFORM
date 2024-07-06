using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_PLATFORM_INFRASTRUCTURE.Migrations
{
    /// <inheritdoc />
    public partial class Vacation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vacations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeEmployee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalVacationDaysLeft = table.Column<int>(type: "int", nullable: false),
                    DaysUsedThisYear = table.Column<int>(type: "int", nullable: false),
                    AdditionalVacationDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentPercentage = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationTypes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vacations");

            migrationBuilder.DropTable(
                name: "VacationTypes");
        }
    }
}
