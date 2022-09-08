using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsDealer.Data.Migrations
{
    public partial class AddCityAndCreatedOnOnCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Cars");
        }
    }
}
