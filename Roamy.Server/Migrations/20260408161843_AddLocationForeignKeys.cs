using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roamy.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ActivityLocations_LocationActivityLocationId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_TripLocations_Trips_TripId",
                table: "TripLocations");

            migrationBuilder.DropIndex(
                name: "IX_Activities_LocationActivityLocationId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "LocationActivityLocationId",
                table: "Activities");

            migrationBuilder.AlterColumn<Guid>(
                name: "TripId",
                table: "TripLocations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ActivityId",
                table: "ActivityLocations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLocations_ActivityId",
                table: "ActivityLocations",
                column: "ActivityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLocations_Activities_ActivityId",
                table: "ActivityLocations",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripLocations_Trips_TripId",
                table: "TripLocations",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLocations_Activities_ActivityId",
                table: "ActivityLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_TripLocations_Trips_TripId",
                table: "TripLocations");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLocations_ActivityId",
                table: "ActivityLocations");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "ActivityLocations");

            migrationBuilder.AlterColumn<Guid>(
                name: "TripId",
                table: "TripLocations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationActivityLocationId",
                table: "Activities",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_LocationActivityLocationId",
                table: "Activities",
                column: "LocationActivityLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ActivityLocations_LocationActivityLocationId",
                table: "Activities",
                column: "LocationActivityLocationId",
                principalTable: "ActivityLocations",
                principalColumn: "ActivityLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TripLocations_Trips_TripId",
                table: "TripLocations",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId");
        }
    }
}
