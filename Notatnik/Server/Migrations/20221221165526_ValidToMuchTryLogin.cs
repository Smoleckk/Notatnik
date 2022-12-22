using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notatnik.Server.Migrations
{
    /// <inheritdoc />
    public partial class ValidToMuchTryLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LoginBlockUntil",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LoginLastTry",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfLoginTry",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginBlockUntil",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LoginLastTry",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NumberOfLoginTry",
                table: "Users");
        }
    }
}
