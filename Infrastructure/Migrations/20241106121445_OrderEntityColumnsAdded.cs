using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderEntityColumnsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PromoCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingChoice",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PromoCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingChoice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");
        }
    }
}
