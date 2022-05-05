using Microsoft.EntityFrameworkCore.Migrations;

namespace TripperAPI.Migrations
{
    public partial class PhotoUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Places_PlaceId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_PlaceId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Photos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PlaceId",
                table: "Photos",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Places_PlaceId",
                table: "Photos",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
