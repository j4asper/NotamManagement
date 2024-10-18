using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotamManagement.Core.Migrations
{
    /// <inheritdoc />
    public partial class add_notamtext_and_radius : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NotamText",
                table: "Notams",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Radius",
                table: "Coordinates",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotamText",
                table: "Notams");

            migrationBuilder.DropColumn(
                name: "Radius",
                table: "Coordinates");
        }
    }
}
