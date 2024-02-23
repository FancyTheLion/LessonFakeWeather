using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeWeatherBackend.DAO.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotosToWeathers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId",
                table: "Weathers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_PhotoId",
                table: "Weathers",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Weathers_Files_PhotoId",
                table: "Weathers",
                column: "PhotoId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weathers_Files_PhotoId",
                table: "Weathers");

            migrationBuilder.DropIndex(
                name: "IX_Weathers_PhotoId",
                table: "Weathers");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Weathers");
        }
    }
}
