using Microsoft.EntityFrameworkCore.Migrations;

namespace TripperAPI.Migrations
{
    public partial class userId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CreatedById",
                table: "Reviews",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_CreatedById",
                table: "Reviews",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_CreatedById",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CreatedById",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
