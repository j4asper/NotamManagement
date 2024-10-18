using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotamManagement.Core.Migrations
{
    /// <inheritdoc />
    public partial class fix_typo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefferenceIdentifier",
                table: "Notams",
                newName: "ReferenceIdentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReferenceIdentifier",
                table: "Notams",
                newName: "RefferenceIdentifier");
        }
    }
}
