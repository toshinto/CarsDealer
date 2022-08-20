using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsDealer.Data.Migrations
{
    public partial class returnbackmessageonoffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Offers");
        }
    }
}
