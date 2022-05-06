using Microsoft.EntityFrameworkCore.Migrations;

namespace TripperAPI.Migrations
{
    public partial class reviewupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Reviews_ReviewId",
                table: "Photos");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewId",
                table: "Photos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Reviews_ReviewId",
                table: "Photos",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Reviews_ReviewId",
                table: "Photos");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewId",
                table: "Photos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Reviews_ReviewId",
                table: "Photos",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
