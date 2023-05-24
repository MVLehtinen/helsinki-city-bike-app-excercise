using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bike_webapi.Migrations
{
    /// <inheritdoc />
    public partial class AddJourneyAndStationModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Osoite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kaupunki = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Operaattori = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kapasiteetti = table.Column<int>(type: "int", nullable: false),
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Journeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Departure = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Return = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureStationId = table.Column<int>(type: "int", nullable: false),
                    ReturnStationId = table.Column<int>(type: "int", nullable: false),
                    CoveredDistance = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journeys_Stations_DepartureStationId",
                        column: x => x.DepartureStationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Journeys_Stations_ReturnStationId",
                        column: x => x.ReturnStationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Journeys_DepartureStationId",
                table: "Journeys",
                column: "DepartureStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Journeys_ReturnStationId",
                table: "Journeys",
                column: "ReturnStationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Journeys");

            migrationBuilder.DropTable(
                name: "Stations");
        }
    }
}
