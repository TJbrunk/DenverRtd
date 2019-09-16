using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RtdData.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StopTimes",
                columns: table => new
                {
                    TripId = table.Column<int>(nullable: false),
                    StopId = table.Column<int>(nullable: false),
                    ArrivalTime = table.Column<TimeSpan>(nullable: false),
                    DepartureTime = table.Column<TimeSpan>(nullable: false),
                    Headsign = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StopTimes", x => new { x.StopId, x.TripId });
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RouteId = table.Column<string>(nullable: true),
                    ServiceId = table.Column<string>(nullable: true),
                    DirectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StopTimes_DepartureTime",
                table: "StopTimes",
                column: "DepartureTime");

            migrationBuilder.CreateIndex(
                name: "IX_StopTimes_StopId",
                table: "StopTimes",
                column: "StopId");

            migrationBuilder.CreateIndex(
                name: "IX_StopTimes_TripId",
                table: "StopTimes",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_RouteId",
                table: "Trips",
                column: "RouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StopTimes");

            migrationBuilder.DropTable(
                name: "Trips");
        }
    }
}
