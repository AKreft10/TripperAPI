using Microsoft.EntityFrameworkCore.Migrations;

namespace TripperAPI.Migrations
{
    public partial class down : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Photos_PhotoId",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Places_PhotoId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Places");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "Places",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Places_PhotoId",
                table: "Places",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Photos_PhotoId",
                table: "Places",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
