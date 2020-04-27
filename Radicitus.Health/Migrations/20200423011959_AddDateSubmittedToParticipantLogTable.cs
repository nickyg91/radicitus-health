using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Radicitus.Health.Migrations
{
    public partial class AddDateSubmittedToParticipantLogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateSubmitted",
                table: "ParticipantLog",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateSubmitted",
                table: "ParticipantLog");
        }
    }
}
