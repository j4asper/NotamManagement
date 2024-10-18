using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotamManagement.Core.Migrations
{
    /// <inheritdoc />
    public partial class fix_Airport_Fligtplan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Airports_FlightPlans_FlightPlanId",
                table: "Airports");

            migrationBuilder.DropIndex(
                name: "IX_Airports_FlightPlanId",
                table: "Airports");

            migrationBuilder.DropColumn(
                name: "FlightPlanId",
                table: "Airports");

            migrationBuilder.CreateTable(
                name: "AirportFlightPlan",
                columns: table => new
                {
                    AirportsId = table.Column<int>(type: "integer", nullable: false),
                    FlightPlansId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirportFlightPlan", x => new { x.AirportsId, x.FlightPlansId });
                    table.ForeignKey(
                        name: "FK_AirportFlightPlan_Airports_AirportsId",
                        column: x => x.AirportsId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AirportFlightPlan_FlightPlans_FlightPlansId",
                        column: x => x.FlightPlansId,
                        principalTable: "FlightPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirportFlightPlan_FlightPlansId",
                table: "AirportFlightPlan",
                column: "FlightPlansId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirportFlightPlan");

            migrationBuilder.AddColumn<int>(
                name: "FlightPlanId",
                table: "Airports",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Airports_FlightPlanId",
                table: "Airports",
                column: "FlightPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Airports_FlightPlans_FlightPlanId",
                table: "Airports",
                column: "FlightPlanId",
                principalTable: "FlightPlans",
                principalColumn: "Id");
        }
    }
}
