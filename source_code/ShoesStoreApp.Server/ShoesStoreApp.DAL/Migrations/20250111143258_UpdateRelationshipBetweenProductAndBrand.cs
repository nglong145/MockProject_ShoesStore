using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesStoreApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationshipBetweenProductAndBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Brand_BrandId1",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_BrandId1",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "BrandId1",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BrandId1",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId1",
                table: "Product",
                column: "BrandId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Brand_BrandId1",
                table: "Product",
                column: "BrandId1",
                principalTable: "Brand",
                principalColumn: "BrandId");
        }
    }
}
