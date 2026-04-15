using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roamy.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixLatitudeTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Latitue",
                table: "TripLocations",
                newName: "Latitude");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "TripLocations",
                newName: "Latitue");
        }
    }
}
