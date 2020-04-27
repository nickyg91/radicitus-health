using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Radicitus.Health.Migrations
{
    public partial class UpdateDbColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCurrent",
                table: "HealthInitiative",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateTime",
                table: "HealthInitiative",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrent",
                table: "HealthInitiative");

            migrationBuilder.DropColumn(
                name: "StartDateTime",
                table: "HealthInitiative");
        }
    }
}
