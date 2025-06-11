using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DBAddCoupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CouponType = table.Column<int>(type: "int", nullable: false),
                    CouponValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_Coupons",
                columns: table => new
                {
                    CouponId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Coupons", x => new { x.CouponId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Product_Coupons_Coupon_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Coupons_Product_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => new { x.CartId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CartItem_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StripePaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("296a460b-3216-4557-983c-58c58d87fdf7"), new DateTime(2024, 3, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), "Các loại tivi, màn hình", "TV & Màn hình" },
                    { new Guid("3156994b-be86-4e69-b0c3-b383ab203a12"), new DateTime(2024, 3, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), "Các loại điện thoại", "Điện thoại" },
                    { new Guid("878dfce8-75e5-4a2b-ba2f-a05322e18239"), new DateTime(2024, 3, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), "Máy lạnh, tủ lạnh", "Đồ gia dụng" },
                    { new Guid("a02a5c5a-f789-4bfe-abff-064ded0a2cde"), new DateTime(2024, 3, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), "Tai nghe, sạc, v.v.", "Phụ kiện" },
                    { new Guid("a9ea7cd2-f475-4a7c-9f25-7116f4073c64"), new DateTime(2024, 3, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), "Máy tính xách tay", "Laptop" }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("00cff030-2921-4267-b802-86dc6d88174a"), "Update category", "Update.Category" },
                    { new Guid("02cd1655-929b-437b-98e5-8a6551eae34d"), "Update order", "Update.Order" },
                    { new Guid("22a895e7-40ac-4168-bc64-def6c6e945a6"), "View order", "View.Order" },
                    { new Guid("238ec90a-8408-4fbe-a991-eb8f2eb4a28c"), "View cart", "View.Cart" },
                    { new Guid("3759092e-5e7b-4ca2-8c83-33104f549a0a"), "Delete category", "Delete.Category" },
                    { new Guid("61d6359f-ce94-4d20-b2d7-66d03ff92891"), "Delete product", "Delete.Product" },
                    { new Guid("6610746a-389f-472e-b4eb-5eef6295b361"), "Update user information", "Update.User" },
                    { new Guid("7b61a427-65a7-459f-a0ed-41154bfaadf5"), "Create product", "Create.Product" },
                    { new Guid("86685765-75fd-4f6d-9f05-46c65ada32d8"), "View category", "View.Category" },
                    { new Guid("8c711aad-83b0-4e9e-af6d-b406ca1b150d"), "Delete cart", "Delete.Cart" },
                    { new Guid("a3ac9aaa-f16a-4100-b07b-e0d4897193db"), "Update product", "Update.Product" },
                    { new Guid("aeb40204-4004-4884-b510-e46742046698"), "Create payment", "Create.Payment" },
                    { new Guid("b248903f-6891-4ebb-8b33-55a3dfa879df"), "Update cart", "Update.Cart" },
                    { new Guid("b4c24c83-5628-4a9f-834b-10b07147481d"), "Update order item", "Update.OrderItem" },
                    { new Guid("b764954a-903a-4ec2-ac11-988e2f9f22ae"), "Create order", "Create.Order" },
                    { new Guid("b84f9ad1-b64f-40b7-8fb2-f71c44ef0106"), "Approve order", "Approve.Order" },
                    { new Guid("b898cc6c-762e-40d4-9a9c-0cd1a3eb7b8e"), "View order item", "View.OrderItem" },
                    { new Guid("c5c65040-fd76-440f-bec0-51f86f19e431"), "Delete order", "Delete.Order" },
                    { new Guid("ccaaebbd-4740-4516-b860-f6440fd4c55f"), "Delete user", "Delete.User" },
                    { new Guid("d57fc2b6-0dc6-4a88-bbaf-bb876aa14310"), "View user information", "View.User" },
                    { new Guid("e1532412-29c1-4e1c-afbf-0f5c5c4e41df"), "Create category", "Create.Category" },
                    { new Guid("e95657da-49a6-49fc-8d6f-384a2dc45cce"), "View product", "View.Product" },
                    { new Guid("fa8fed50-24a7-4871-84d7-20b2376e8e27"), "View payment", "View.Payment" },
                    { new Guid("fcadd358-388c-4d20-a5af-86a235cf4352"), "Update payment", "Update.Payment" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), null, "Admin" },
                    { new Guid("a6f25e26-400e-4e55-97b3-94ac35fd32ee"), null, "Customer" },
                    { new Guid("fcd2fb03-484d-4c67-9940-bf4668619e9d"), null, "Manager" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "Phone" },
                values: new object[,]
                {
                    { new Guid("35e3a23d-4ac9-4282-89e9-5ea690da8458"), new DateTime(2024, 3, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), "minhquang85213@gmail.com", "Quang", "$2a$11$FkdaLqGjhB0K5sPGuD71NOa0ggTcYkn/R.b8UWBKbHyxRSBruexR.", "0789653241" },
                    { new Guid("d1407a13-ad60-48ad-8346-8fba9cbb4f41"), new DateTime(2024, 3, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), "Thanh123@gmail.com", "Thanh", "$2a$11$0/CP8hh.odVCJCJi0d261ObBVpXQ06FuX53Aiq6Fn.0pKKdcdnMz2", "0985632147" }
                });

            migrationBuilder.InsertData(
                table: "Cart",
                columns: new[] { "Id", "CreatedAt", "TotalAmount", "UserId" },
                values: new object[] { new Guid("560a88f4-f3e5-40e6-8076-5cd6780dc14a"), new DateTime(2024, 3, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), 0m, new Guid("d1407a13-ad60-48ad-8346-8fba9cbb4f41") });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("4e3d18ea-474a-4a7d-96d9-89e6545d626b"), new Guid("a02a5c5a-f789-4bfe-abff-064ded0a2cde"), new DateTime(2024, 3, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), "Tai nghe không dây với chất lượng âm thanh tốt và chống ồn chủ động", "Tai nghe AirPods", 4990000m, 100 },
                    { new Guid("a1034129-c1fd-42d5-9773-b878fde40e1a"), new Guid("a9ea7cd2-f475-4a7c-9f25-7116f4073c64"), new DateTime(2024, 3, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), "Ultrabook cao cấp với màn hình 4K OLED và CPU Intel Core i9", "Dell XPS 15", 45990000m, 25 },
                    { new Guid("c107fe9b-ae3e-4b74-bb43-0d390c79d486"), new Guid("3156994b-be86-4e69-b0c3-b383ab203a12"), new DateTime(2024, 3, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), "Điện thoại cao cấp với camera 48MP và chip A16 Bionic", "iPhone 14 Pro", 27990000m, 50 },
                    { new Guid("f19411a4-e9dd-41af-9773-73051ab930a5"), new Guid("3156994b-be86-4e69-b0c3-b383ab203a12"), new DateTime(2024, 3, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), "Smartphone flagship với bút S-Pen và camera 200MP", "Samsung S23 Ultra", 30990000m, 40 }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("00cff030-2921-4267-b802-86dc6d88174a"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("02cd1655-929b-437b-98e5-8a6551eae34d"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("22a895e7-40ac-4168-bc64-def6c6e945a6"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("238ec90a-8408-4fbe-a991-eb8f2eb4a28c"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("3759092e-5e7b-4ca2-8c83-33104f549a0a"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("61d6359f-ce94-4d20-b2d7-66d03ff92891"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("6610746a-389f-472e-b4eb-5eef6295b361"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("7b61a427-65a7-459f-a0ed-41154bfaadf5"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("86685765-75fd-4f6d-9f05-46c65ada32d8"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("8c711aad-83b0-4e9e-af6d-b406ca1b150d"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("a3ac9aaa-f16a-4100-b07b-e0d4897193db"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("aeb40204-4004-4884-b510-e46742046698"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("b248903f-6891-4ebb-8b33-55a3dfa879df"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("b4c24c83-5628-4a9f-834b-10b07147481d"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("b764954a-903a-4ec2-ac11-988e2f9f22ae"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("b84f9ad1-b64f-40b7-8fb2-f71c44ef0106"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("b898cc6c-762e-40d4-9a9c-0cd1a3eb7b8e"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("c5c65040-fd76-440f-bec0-51f86f19e431"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("ccaaebbd-4740-4516-b860-f6440fd4c55f"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("d57fc2b6-0dc6-4a88-bbaf-bb876aa14310"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("e1532412-29c1-4e1c-afbf-0f5c5c4e41df"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("e95657da-49a6-49fc-8d6f-384a2dc45cce"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("fa8fed50-24a7-4871-84d7-20b2376e8e27"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("fcadd358-388c-4d20-a5af-86a235cf4352"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") },
                    { new Guid("22a895e7-40ac-4168-bc64-def6c6e945a6"), new Guid("a6f25e26-400e-4e55-97b3-94ac35fd32ee") },
                    { new Guid("238ec90a-8408-4fbe-a991-eb8f2eb4a28c"), new Guid("a6f25e26-400e-4e55-97b3-94ac35fd32ee") },
                    { new Guid("b248903f-6891-4ebb-8b33-55a3dfa879df"), new Guid("a6f25e26-400e-4e55-97b3-94ac35fd32ee") },
                    { new Guid("b764954a-903a-4ec2-ac11-988e2f9f22ae"), new Guid("a6f25e26-400e-4e55-97b3-94ac35fd32ee") },
                    { new Guid("02cd1655-929b-437b-98e5-8a6551eae34d"), new Guid("fcd2fb03-484d-4c67-9940-bf4668619e9d") },
                    { new Guid("22a895e7-40ac-4168-bc64-def6c6e945a6"), new Guid("fcd2fb03-484d-4c67-9940-bf4668619e9d") },
                    { new Guid("b84f9ad1-b64f-40b7-8fb2-f71c44ef0106"), new Guid("fcd2fb03-484d-4c67-9940-bf4668619e9d") },
                    { new Guid("e95657da-49a6-49fc-8d6f-384a2dc45cce"), new Guid("fcd2fb03-484d-4c67-9940-bf4668619e9d") }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("fcd2fb03-484d-4c67-9940-bf4668619e9d"), new Guid("35e3a23d-4ac9-4282-89e9-5ea690da8458") },
                    { new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), new Guid("d1407a13-ad60-48ad-8346-8fba9cbb4f41") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_OrderId",
                table: "Payment",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Product_Coupons");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
