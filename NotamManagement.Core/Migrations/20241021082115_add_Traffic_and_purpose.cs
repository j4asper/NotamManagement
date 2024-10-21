using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotamManagement.Core.Migrations
{
    /// <inheritdoc />
    public partial class add_Traffic_and_purpose : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotamActions_Notams_NotamId",
                table: "NotamActions");

            migrationBuilder.DropIndex(
                name: "IX_NotamActions_NotamId",
                table: "NotamActions");

            migrationBuilder.AddColumn<string>(
                name: "Scope",
                table: "Notams",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Traffic",
                table: "Notams",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Scope",
                table: "Notams");

            migrationBuilder.DropColumn(
                name: "Traffic",
                table: "Notams");

            migrationBuilder.CreateIndex(
                name: "IX_NotamActions_NotamId",
                table: "NotamActions",
                column: "NotamId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotamActions_Notams_NotamId",
                table: "NotamActions",
                column: "NotamId",
                principalTable: "Notams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
