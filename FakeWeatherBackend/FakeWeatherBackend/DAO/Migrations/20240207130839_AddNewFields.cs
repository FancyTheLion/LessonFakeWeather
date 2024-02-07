using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeWeatherBackend.DAO.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Humidity",
                table: "Weathers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Precipitation",
                table: "Weathers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Pressure",
                table: "Weathers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Humidity",
                table: "Weathers");

            migrationBuilder.DropColumn(
                name: "Precipitation",
                table: "Weathers");

            migrationBuilder.DropColumn(
                name: "Pressure",
                table: "Weathers");
        }
    }
}
