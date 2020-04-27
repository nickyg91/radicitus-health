using Microsoft.EntityFrameworkCore.Migrations;

namespace Radicitus.Health.Migrations
{
    public partial class AddStartingWeightToParticipant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "StartingWeight",
                table: "HealthParticipant",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartingWeight",
                table: "HealthParticipant");
        }
    }
}
