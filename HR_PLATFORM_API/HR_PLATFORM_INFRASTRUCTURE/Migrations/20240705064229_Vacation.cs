using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_PLATFORM_INFRASTRUCTURE.Migrations
{
    /// <inheritdoc />
    public partial class Vacation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "StatutEmployee",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatutEmployee",
                table: "Employees");
        }
    }
}
