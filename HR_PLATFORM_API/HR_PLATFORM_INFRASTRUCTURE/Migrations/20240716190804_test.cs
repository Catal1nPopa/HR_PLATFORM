using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_PLATFORM_INFRASTRUCTURE.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "Employees",
                newName: "ContractCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContractCode",
                table: "Employees",
                newName: "Salary");
        }
    }
}
