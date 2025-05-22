using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbUpdatePermission2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("34ae215d-c182-4c9f-b745-b8986d36dc36"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("3ef0841e-bd88-4e27-8642-603c805462f9"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("95747c1e-fc1f-48ec-b108-34af7da5bce2"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("b867733f-fbbb-410b-b7f6-0c667d40a229"));

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("00cff030-2921-4267-b802-86dc6d88174a"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("02cd1655-929b-437b-98e5-8a6551eae34d"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("22a895e7-40ac-4168-bc64-def6c6e945a6"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("238ec90a-8408-4fbe-a991-eb8f2eb4a28c"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("3759092e-5e7b-4ca2-8c83-33104f549a0a"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("61d6359f-ce94-4d20-b2d7-66d03ff92891"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("6610746a-389f-472e-b4eb-5eef6295b361"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("7b61a427-65a7-459f-a0ed-41154bfaadf5"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("86685765-75fd-4f6d-9f05-46c65ada32d8"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("8c711aad-83b0-4e9e-af6d-b406ca1b150d"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a3ac9aaa-f16a-4100-b07b-e0d4897193db"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("aeb40204-4004-4884-b510-e46742046698"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("b248903f-6891-4ebb-8b33-55a3dfa879df"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("b4c24c83-5628-4a9f-834b-10b07147481d"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("b764954a-903a-4ec2-ac11-988e2f9f22ae"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("b84f9ad1-b64f-40b7-8fb2-f71c44ef0106"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("b898cc6c-762e-40d4-9a9c-0cd1a3eb7b8e"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("c5c65040-fd76-440f-bec0-51f86f19e431"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ccaaebbd-4740-4516-b860-f6440fd4c55f"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d57fc2b6-0dc6-4a88-bbaf-bb876aa14310"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("e1532412-29c1-4e1c-afbf-0f5c5c4e41df"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("e95657da-49a6-49fc-8d6f-384a2dc45cce"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("fa8fed50-24a7-4871-84d7-20b2376e8e27"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("fcadd358-388c-4d20-a5af-86a235cf4352"), new Guid("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("22a895e7-40ac-4168-bc64-def6c6e945a6"), new Guid("a6f25e26-400e-4e55-97b3-94ac35fd32ee") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("238ec90a-8408-4fbe-a991-eb8f2eb4a28c"), new Guid("a6f25e26-400e-4e55-97b3-94ac35fd32ee") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("b248903f-6891-4ebb-8b33-55a3dfa879df"), new Guid("a6f25e26-400e-4e55-97b3-94ac35fd32ee") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("b764954a-903a-4ec2-ac11-988e2f9f22ae"), new Guid("a6f25e26-400e-4e55-97b3-94ac35fd32ee") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("02cd1655-929b-437b-98e5-8a6551eae34d"), new Guid("fcd2fb03-484d-4c67-9940-bf4668619e9d") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("22a895e7-40ac-4168-bc64-def6c6e945a6"), new Guid("fcd2fb03-484d-4c67-9940-bf4668619e9d") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("b84f9ad1-b64f-40b7-8fb2-f71c44ef0106"), new Guid("fcd2fb03-484d-4c67-9940-bf4668619e9d") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("e95657da-49a6-49fc-8d6f-384a2dc45cce"), new Guid("fcd2fb03-484d-4c67-9940-bf4668619e9d") });

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("00cff030-2921-4267-b802-86dc6d88174a"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("02cd1655-929b-437b-98e5-8a6551eae34d"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("22a895e7-40ac-4168-bc64-def6c6e945a6"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("238ec90a-8408-4fbe-a991-eb8f2eb4a28c"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("3759092e-5e7b-4ca2-8c83-33104f549a0a"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("61d6359f-ce94-4d20-b2d7-66d03ff92891"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("6610746a-389f-472e-b4eb-5eef6295b361"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("7b61a427-65a7-459f-a0ed-41154bfaadf5"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("86685765-75fd-4f6d-9f05-46c65ada32d8"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("8c711aad-83b0-4e9e-af6d-b406ca1b150d"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("a3ac9aaa-f16a-4100-b07b-e0d4897193db"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("aeb40204-4004-4884-b510-e46742046698"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("b248903f-6891-4ebb-8b33-55a3dfa879df"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("b4c24c83-5628-4a9f-834b-10b07147481d"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("b764954a-903a-4ec2-ac11-988e2f9f22ae"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("b84f9ad1-b64f-40b7-8fb2-f71c44ef0106"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("b898cc6c-762e-40d4-9a9c-0cd1a3eb7b8e"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("c5c65040-fd76-440f-bec0-51f86f19e431"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("ccaaebbd-4740-4516-b860-f6440fd4c55f"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("d57fc2b6-0dc6-4a88-bbaf-bb876aa14310"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("e1532412-29c1-4e1c-afbf-0f5c5c4e41df"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("e95657da-49a6-49fc-8d6f-384a2dc45cce"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("fa8fed50-24a7-4871-84d7-20b2376e8e27"));

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: new Guid("fcadd358-388c-4d20-a5af-86a235cf4352"));

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("34ae215d-c182-4c9f-b745-b8986d36dc36"), "Modify data", "Update" },
                    { new Guid("3ef0841e-bd88-4e27-8642-603c805462f9"), "Add new data", "Create" },
                    { new Guid("95747c1e-fc1f-48ec-b108-34af7da5bce2"), "Remove data", "Delete" },
                    { new Guid("b867733f-fbbb-410b-b7f6-0c667d40a229"), "View data", "Read" }
                });
        }
    }
}
