using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbupdateHasData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cart",
                columns: new[] { "Id", "CreatedAt", "TotalAmount", "UserId" },
                values: new object[] { new Guid("560a88f4-f3e5-40e6-8076-5cd6780dc14a"), new DateTime(2025, 5, 21, 10, 31, 33, 340, DateTimeKind.Utc).AddTicks(9112), 0m, new Guid("d1407a13-ad60-48ad-8346-8fba9cbb4f41") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cart",
                keyColumn: "Id",
                keyValue: new Guid("560a88f4-f3e5-40e6-8076-5cd6780dc14a"));
        }
    }
}
