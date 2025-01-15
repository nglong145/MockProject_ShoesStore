using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoesStoreApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabasehai : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("052f453f-ab51-4035-bcfe-467ba4506c22"), null, "Admin", "ADMIN" },
                    { new Guid("f45d405b-e271-484a-b270-c6cf17a561d6"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("2bfc4217-b6af-4855-9529-7f01c9331629"), 0, "Default Address", "ba0aa5bd-e275-42ce-9a80-6965b6dcad3c", "admin@gmail.com", true, "Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEMqkISYVp44ZWWmyIg9yG9q3sF09AR1S/bKrfD7rmR00ymd7tyF+LFPrQGhJJxUs+Q==", null, false, new Guid("052f453f-ab51-4035-bcfe-467ba4506c22"), null, true, false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f45d405b-e271-484a-b270-c6cf17a561d6"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2bfc4217-b6af-4855-9529-7f01c9331629"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("052f453f-ab51-4035-bcfe-467ba4506c22"));

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
