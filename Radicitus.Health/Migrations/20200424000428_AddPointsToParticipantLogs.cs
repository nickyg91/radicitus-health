using Microsoft.EntityFrameworkCore.Migrations;

namespace Radicitus.Health.Migrations
{
    public partial class AddPointsToParticipantLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Points",
                table: "ParticipantLog",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "ParticipantLog");
        }
    }
}
