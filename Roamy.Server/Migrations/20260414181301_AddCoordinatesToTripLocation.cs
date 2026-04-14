using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roamy.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddCoordinatesToTripLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitue",
                table: "TripLocations",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "TripLocations",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitue",
                table: "TripLocations");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "TripLocations");
        }
    }
}
