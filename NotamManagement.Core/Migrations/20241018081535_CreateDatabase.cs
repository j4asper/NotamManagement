using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NotamManagement.Core.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EffectiveValidTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    NotamOffice = table.Column<string>(type: "text", nullable: false),
                    QCode = table.Column<string>(type: "text", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    RefferenceIdentifier = table.Column<string>(type: "text", nullable: false),
                    FIR = table.Column<string>(type: "text", nullable: false),
                    Purpose = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coordinates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NotamId = table.Column<int>(type: "integer", nullable: false),
                    Longitude = table.Column<float>(type: "real", nullable: false),
                    Latitude = table.Column<float>(type: "real", nullable: false),
                    UpperFlightLevel = table.Column<int>(type: "integer", nullable: false),
                    LowerFlightLevel = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coordinates_Notams_NotamId",
                        column: x => x.NotamId,
                        principalTable: "Notams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightPlans_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotamActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    NotamId = table.Column<int>(type: "integer", nullable: false),
                    Importance = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    OrganizationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotamActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotamActions_Notams_NotamId",
                        column: x => x.NotamId,
                        principalTable: "Notams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotamActions_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    OrganizationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ICAO = table.Column<string>(type: "text", nullable: false),
                    FIR = table.Column<string>(type: "text", nullable: false),
                    FlightPlanId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Airports_FlightPlans_FlightPlanId",
                        column: x => x.FlightPlanId,
                        principalTable: "FlightPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Airports_FlightPlanId",
                table: "Airports",
                column: "FlightPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Coordinates_NotamId",
                table: "Coordinates",
                column: "NotamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlightPlans_OrganizationId",
                table: "FlightPlans",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotamActions_NotamId",
                table: "NotamActions",
                column: "NotamId");

            migrationBuilder.CreateIndex(
                name: "IX_NotamActions_OrganizationId",
                table: "NotamActions",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                table: "Users",
                column: "OrganizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "Coordinates");

            migrationBuilder.DropTable(
                name: "NotamActions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "FlightPlans");

            migrationBuilder.DropTable(
                name: "Notams");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
