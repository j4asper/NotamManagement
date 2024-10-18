﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotamManagement.Core.Migrations
{
    /// <inheritdoc />
    public partial class fix_refferenceIdentifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefferenceIdentifier",
                table: "Notams",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefferenceIdentifier",
                table: "Notams",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
