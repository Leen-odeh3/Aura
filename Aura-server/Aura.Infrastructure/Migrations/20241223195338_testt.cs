using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aura.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class testt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Stories");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Stories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stories_ImageId",
                table: "Stories",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Images_ImageId",
                table: "Stories",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Images_ImageId",
                table: "Stories");

            migrationBuilder.DropIndex(
                name: "IX_Stories_ImageId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Stories");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
