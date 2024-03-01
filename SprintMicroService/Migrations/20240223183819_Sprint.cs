using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SprintMicroService.Migrations
{
    /// <inheritdoc />
    public partial class Sprint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    SprintId = table.Column<Guid>(type: "uuid", nullable: false),
                    SprintName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SprintGoals = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    SprintDescription = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SprintStartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    SprintEndDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    SuggestionsForNextSprint = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    SprintNotes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    GoalsAchieved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.SprintId);
                });

            migrationBuilder.CreateTable(
                name: "Phases",
                columns: table => new
                {
                    PhaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    PhaseName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PhaseDescription = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PhaseStartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    PhaseEndDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    PhaseNotes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PhaseGoalsAchieved = table.Column<bool>(type: "boolean", nullable: false),
                    SprintId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phases", x => x.PhaseId);
                    table.ForeignKey(
                        name: "FK_Phases_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprints",
                        principalColumn: "SprintId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Sprints",
                columns: new[] { "SprintId", "GoalsAchieved", "SprintDescription", "SprintEndDate", "SprintGoals", "SprintName", "SprintNotes", "SprintStartDate", "SuggestionsForNextSprint" },
                values: new object[,]
                {
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), true, "Opis prvog sprinta", new DateTimeOffset(new DateTime(2023, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Ciljevi prvog sprinta", "Sprint 1", "Napomene o prvom sprintu", new DateTimeOffset(new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Sugestije za prvi sprint" },
                    { new Guid("f47c88b9-70e4-4b1b-bb70-1f02c2f5a5e1"), true, "Opis drugog sprinta", new DateTimeOffset(new DateTime(2023, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Ciljevi drugog sprinta", "Sprint 2", "Napomene o drugom sprintu", new DateTimeOffset(new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Sugestije za drugi sprint" }
                });

            migrationBuilder.InsertData(
                table: "Phases",
                columns: new[] { "PhaseId", "PhaseDescription", "PhaseEndDate", "PhaseGoalsAchieved", "PhaseName", "PhaseNotes", "PhaseStartDate", "SprintId" },
                values: new object[,]
                {
                    { new Guid("4b3f8c15-2ee8-46c9-8e25-29d7a3977f92"), "Execution phase for project XYZ", new DateTimeOffset(new DateTime(2023, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), true, "Execution", "Notes for the execution phase", new DateTimeOffset(new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new Guid("f47c88b9-70e4-4b1b-bb70-1f02c2f5a5e1") },
                    { new Guid("a45e2e02-38f6-4a56-94f5-74a1cf751087"), "Planning phase for project X", new DateTimeOffset(new DateTime(2023, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), false, "Planning", "Notes for the planning phase", new DateTimeOffset(new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new Guid("f47c88b9-70e4-4b1b-bb70-1f02c2f5a5e1") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phases_SprintId",
                table: "Phases",
                column: "SprintId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phases");

            migrationBuilder.DropTable(
                name: "Sprints");
        }
    }
}
