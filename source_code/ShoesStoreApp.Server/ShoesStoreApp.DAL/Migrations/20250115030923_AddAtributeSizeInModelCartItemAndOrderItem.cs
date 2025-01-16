using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoesStoreApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAtributeSizeInModelCartItemAndOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b662afc0-366c-4c2a-bd25-665bfcc463d1"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ea342e44-69b9-486f-8d04-6b50c222e1ab"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bc58f2e0-0366-4955-8e70-68ec9729fb24"));

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "OrderItem",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "CartItem",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem",
                columns: new[] { "OrderId", "ProductId", "Size" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem",
                columns: new[] { "CartId", "ProductId", "Size" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("430bcc86-8ece-4e88-a25b-4ef8ac0bac8a"), null, "Admin", "ADMIN" },
                    { new Guid("a4ab3477-e232-402e-8f62-23ee2411b093"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("5312509f-39c4-4c96-a677-353b07621596"), 0, "Default Address", "02e5f0b1-ec46-495d-88b2-72b7621a10d2", "admin@gmail.com", true, "Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAECQNX6FRO5gC2JEwNzDXugy5PxSKHDsdw7VUIIifiAA7rfc9N85hNqNPOqdlUEyLOA==", null, false, new Guid("430bcc86-8ece-4e88-a25b-4ef8ac0bac8a"), null, true, false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a4ab3477-e232-402e-8f62-23ee2411b093"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5312509f-39c4-4c96-a677-353b07621596"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("430bcc86-8ece-4e88-a25b-4ef8ac0bac8a"));

            migrationBuilder.DropColumn(
                name: "Size",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "CartItem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem",
                columns: new[] { "CartId", "ProductId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("b662afc0-366c-4c2a-bd25-665bfcc463d1"), null, "User", "USER" },
                    { new Guid("bc58f2e0-0366-4955-8e70-68ec9729fb24"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("ea342e44-69b9-486f-8d04-6b50c222e1ab"), 0, "Default Address", "74293286-8530-4894-8ec2-51085bd1ca9d", "admin@gmail.com", true, "Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEF4U9UcMqZo0xvd2QeP+e/x3MEVJuoCjS+FrHb9nzjYdCuRyKaER2rZmBc2W+HIBag==", null, false, new Guid("bc58f2e0-0366-4955-8e70-68ec9729fb24"), null, true, false, "admin" });
        }
    }
}
