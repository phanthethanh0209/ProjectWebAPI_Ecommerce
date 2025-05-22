using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbupdateHasData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "Id",
                keyValue: new Guid("560a88f4-f3e5-40e6-8076-5cd6780dc14a"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 22, 12, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cart",
                keyColumn: "Id",
                keyValue: new Guid("560a88f4-f3e5-40e6-8076-5cd6780dc14a"),
                column: "CreatedAt",
                value: new DateTime(2025, 5, 21, 10, 31, 33, 340, DateTimeKind.Utc).AddTicks(9112));
        }
    }
}
