using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditProfileAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("3e8e1117-81b7-49aa-9961-d9bcc0a26266"),
                column: "Email",
                value: "phanthethanh2003@gmail.com");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("3e8e1117-81b7-49aa-9961-d9bcc0a26266"),
                column: "Email",
                value: "Thanh123@gmail.com");
        }
    }
}
