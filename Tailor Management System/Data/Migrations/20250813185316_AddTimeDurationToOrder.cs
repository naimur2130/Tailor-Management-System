using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tailor_Management_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeDurationToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TimeDuration",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeDuration",
                table: "Order");
        }
    }
}
