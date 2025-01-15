using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoesStoreApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initiadb : Migration
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
                    { new Guid("41413663-4c60-4068-93ab-591737d13dd5"), null, "Admin", "ADMIN" },
                    { new Guid("4dd47138-8857-4fd2-942e-f1f50f60818c"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("582e216c-2dbc-49b7-9407-879056ecd0e0"), 0, "Default Address", "8a35d63e-9910-4cd5-954b-dde115f84222", "admin@gmail.com", true, "Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAELrDT4pYzvJR6qvLn6gDj0Ns/tEUBJbSmimBWKn8DNAYcCdlvcllN7l8VUTBVRxDUw==", null, false, new Guid("41413663-4c60-4068-93ab-591737d13dd5"), null, true, false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4dd47138-8857-4fd2-942e-f1f50f60818c"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("582e216c-2dbc-49b7-9407-879056ecd0e0"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("41413663-4c60-4068-93ab-591737d13dd5"));

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
