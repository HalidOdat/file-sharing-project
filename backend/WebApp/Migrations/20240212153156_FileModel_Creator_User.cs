using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class FileModel_Creator_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileModels_AspNetUsers_ApplicationUserId",
                table: "FileModels");

            migrationBuilder.DropIndex(
                name: "IX_FileModels_ApplicationUserId",
                table: "FileModels");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "FileModels");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "FileModels",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FileModels_CreatorId",
                table: "FileModels",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileModels_AspNetUsers_CreatorId",
                table: "FileModels",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileModels_AspNetUsers_CreatorId",
                table: "FileModels");

            migrationBuilder.DropIndex(
                name: "IX_FileModels_CreatorId",
                table: "FileModels");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "FileModels");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "FileModels",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileModels_ApplicationUserId",
                table: "FileModels",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileModels_AspNetUsers_ApplicationUserId",
                table: "FileModels",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
