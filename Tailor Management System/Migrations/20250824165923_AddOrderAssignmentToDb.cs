using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tailor_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderAssignmentToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderAssignment",
                columns: table => new
                {
                    OrderAssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAssignment", x => x.OrderAssignmentId);
                    table.ForeignKey(
                        name: "FK_OrderAssignment_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderAssignment_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderAssignment_EmployeeId",
                table: "OrderAssignment",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAssignment_OrderId",
                table: "OrderAssignment",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderAssignment");
        }
    }
}
