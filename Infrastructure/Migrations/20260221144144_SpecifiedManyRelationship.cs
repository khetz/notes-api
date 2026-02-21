using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SpecifiedManyRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Categories_CategoryId1",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_UserId1",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_CategoryId1",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_UserId1",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Notes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CategoryId1",
                table: "Notes",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId1",
                table: "Notes",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Categories_CategoryId1",
                table: "Notes",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_UserId1",
                table: "Notes",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
