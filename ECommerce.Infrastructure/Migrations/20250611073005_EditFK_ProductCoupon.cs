using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditFK_ProductCoupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Coupons_Product_CouponId",
                table: "Product_Coupons");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Coupons_ProductId",
                table: "Product_Coupons",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Coupons_Product_ProductId",
                table: "Product_Coupons",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Coupons_Product_ProductId",
                table: "Product_Coupons");

            migrationBuilder.DropIndex(
                name: "IX_Product_Coupons_ProductId",
                table: "Product_Coupons");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Coupons_Product_CouponId",
                table: "Product_Coupons",
                column: "CouponId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
