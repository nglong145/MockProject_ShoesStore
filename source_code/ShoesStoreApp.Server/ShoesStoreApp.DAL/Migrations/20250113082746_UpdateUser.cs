using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoesStoreApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9702c5f4-757a-4310-b12c-06b8ba145c32"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cb1a9b67-658e-4b75-b98d-a3797982beb0"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("085dad78-878b-4772-af4e-be07991e3e6e"));

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("085dad78-878b-4772-af4e-be07991e3e6e"), null, "Admin", "ADMIN" },
                    { new Guid("9702c5f4-757a-4310-b12c-06b8ba145c32"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("cb1a9b67-658e-4b75-b98d-a3797982beb0"), 0, "Default Address", "349c57d9-c4b6-41b0-a915-d434c585cbff", "admin@gmail.com", true, "Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEGKj8ugb5fFmDI37PWMROG53iwj51PJF+Kj43jEC1hI1XqeEy3ZnM43HyYDgQnS+dA==", null, false, new Guid("085dad78-878b-4772-af4e-be07991e3e6e"), null, true, false, "admin" });
        }
    }
}
