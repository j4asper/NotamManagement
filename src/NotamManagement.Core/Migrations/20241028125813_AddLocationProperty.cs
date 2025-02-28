using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotamManagement.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotamActions_Organizations_OrganizationId",
                table: "NotamActions");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Notams",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "NotamActions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NotamActions_Organizations_OrganizationId",
                table: "NotamActions",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotamActions_Organizations_OrganizationId",
                table: "NotamActions");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Notams");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "NotamActions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_NotamActions_Organizations_OrganizationId",
                table: "NotamActions",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");
        }
    }
}
