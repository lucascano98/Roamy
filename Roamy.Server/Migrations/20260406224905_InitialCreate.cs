using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roamy.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                });

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    DayId = table.Column<Guid>(type: "uuid", nullable: false),
                    TripId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DayNumber = table.Column<int>(type: "integer", nullable: false),
                    Weather = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.DayId);
                    table.ForeignKey(
                        name: "FK_Days_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripLocations",
                columns: table => new
                {
                    TripLocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    TripId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripLocations", x => x.TripLocationId);
                    table.ForeignKey(
                        name: "FK_TripLocations_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId");
                });

            migrationBuilder.CreateTable(
                name: "ActivityLocations",
                columns: table => new
                {
                    ActivityLocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    AreaTripLocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLocations", x => x.ActivityLocationId);
                    table.ForeignKey(
                        name: "FK_ActivityLocations_TripLocations_AreaTripLocationId",
                        column: x => x.AreaTripLocationId,
                        principalTable: "TripLocations",
                        principalColumn: "TripLocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<Guid>(type: "uuid", nullable: false),
                    DayId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: true),
                    LocationActivityLocationId = table.Column<Guid>(type: "uuid", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: true),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    TripId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_Activities_ActivityLocations_LocationActivityLocationId",
                        column: x => x.LocationActivityLocationId,
                        principalTable: "ActivityLocations",
                        principalColumn: "ActivityLocationId");
                    table.ForeignKey(
                        name: "FK_Activities_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "DayId");
                    table.ForeignKey(
                        name: "FK_Activities_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_DayId",
                table: "Activities",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_LocationActivityLocationId",
                table: "Activities",
                column: "LocationActivityLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_TripId",
                table: "Activities",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLocations_AreaTripLocationId",
                table: "ActivityLocations",
                column: "AreaTripLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Days_TripId",
                table: "Days",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripLocations_TripId",
                table: "TripLocations",
                column: "TripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "ActivityLocations");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "TripLocations");

            migrationBuilder.DropTable(
                name: "Trips");
        }
    }
}
