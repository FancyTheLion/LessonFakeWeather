using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeWeatherBackend.DAO.Migrations
{
    /// <inheritdoc />
    public partial class PrecipitationDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precipitation",
                table: "Weathers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Precipitation",
                table: "Weathers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
