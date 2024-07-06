using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_PLATFORM_INFRASTRUCTURE.Migrations
{
    /// <inheritdoc />
    public partial class Vacation22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalVacationDays",
                table: "Vacations");

            migrationBuilder.RenameColumn(
                name: "TotalVacationDaysLeft",
                table: "Vacations",
                newName: "VacationDaysLeft");

            migrationBuilder.RenameColumn(
                name: "DaysUsedThisYear",
                table: "Vacations",
                newName: "DaysVacation");

            migrationBuilder.RenameColumn(
                name: "CodeEmployee",
                table: "Vacations",
                newName: "TypeVacation");

            migrationBuilder.AddColumn<string>(
                name: "CodEmployee",
                table: "Vacations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "Vacations",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "Vacations",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateTable(
                name: "VacationsDetails",
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
                    table.PrimaryKey("PK_VacationsDetails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacationsDetails");

            migrationBuilder.DropColumn(
                name: "CodEmployee",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Vacations");

            migrationBuilder.RenameColumn(
                name: "VacationDaysLeft",
                table: "Vacations",
                newName: "TotalVacationDaysLeft");

            migrationBuilder.RenameColumn(
                name: "TypeVacation",
                table: "Vacations",
                newName: "CodeEmployee");

            migrationBuilder.RenameColumn(
                name: "DaysVacation",
                table: "Vacations",
                newName: "DaysUsedThisYear");

            migrationBuilder.AddColumn<int>(
                name: "AdditionalVacationDays",
                table: "Vacations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
