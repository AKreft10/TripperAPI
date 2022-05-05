using Microsoft.EntityFrameworkCore.Migrations;

namespace TripperAPI.Migrations
{
    public partial class TypoFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitute",
                table: "Addresses",
                newName: "Longitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Addresses",
                newName: "Longitute");
        }
    }
}
