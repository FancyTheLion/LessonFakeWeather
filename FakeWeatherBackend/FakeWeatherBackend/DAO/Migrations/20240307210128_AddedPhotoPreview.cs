using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeWeatherBackend.DAO.Migrations
{
    /// <inheritdoc />
    public partial class AddedPhotoPreview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PhotoPreviewId",
                table: "Weathers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_PhotoPreviewId",
                table: "Weathers",
                column: "PhotoPreviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Weathers_Files_PhotoPreviewId",
                table: "Weathers",
                column: "PhotoPreviewId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weathers_Files_PhotoPreviewId",
                table: "Weathers");

            migrationBuilder.DropIndex(
                name: "IX_Weathers_PhotoPreviewId",
                table: "Weathers");

            migrationBuilder.DropColumn(
                name: "PhotoPreviewId",
                table: "Weathers");
        }
    }
}
