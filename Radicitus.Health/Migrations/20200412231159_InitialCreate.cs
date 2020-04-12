using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Radicitus.Health.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthInitiative",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    TotalWeightLossGoal = table.Column<decimal>(type: "decimal(4,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthInitiative", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealthParticipant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HealthInitiativeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    IndividualWeightLossGoal = table.Column<decimal>(type: "decimal(4,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthParticipant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthParticipant_HealthInitiative_HealthInitiativeId",
                        column: x => x.HealthInitiativeId,
                        principalTable: "HealthInitiative",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParticipantId = table.Column<int>(nullable: false),
                    HealthInitiativeId = table.Column<int>(nullable: false),
                    Photo = table.Column<byte[]>(nullable: true),
                    CurrentWeight = table.Column<decimal>(type: "decimal(4,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantLog_HealthInitiative_HealthInitiativeId",
                        column: x => x.HealthInitiativeId,
                        principalTable: "HealthInitiative",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantLog_HealthParticipant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "HealthParticipant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthParticipant_HealthInitiativeId",
                table: "HealthParticipant",
                column: "HealthInitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantLog_HealthInitiativeId",
                table: "ParticipantLog",
                column: "HealthInitiativeId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantLog_ParticipantId",
                table: "ParticipantLog",
                column: "ParticipantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipantLog");

            migrationBuilder.DropTable(
                name: "HealthParticipant");

            migrationBuilder.DropTable(
                name: "HealthInitiative");
        }
    }
}
