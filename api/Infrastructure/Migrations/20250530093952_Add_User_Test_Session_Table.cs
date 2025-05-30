using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_User_Test_Session_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingMemoryResponses_Users_StudentId",
                table: "WorkingMemoryResponses");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "WorkingMemoryResponses",
                newName: "UserTestSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingMemoryResponses_StudentId",
                table: "WorkingMemoryResponses",
                newName: "IX_WorkingMemoryResponses_UserTestSessionId");

            migrationBuilder.AddColumn<int>(
                name: "BlockNumber",
                table: "WorkingMemoryTerms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserTestSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    WorkingMemoryTestId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTestSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTestSessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTestSessions_WorkingMemoryTests_WorkingMemoryTestId",
                        column: x => x.WorkingMemoryTestId,
                        principalTable: "WorkingMemoryTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTestSessions_UserId",
                table: "UserTestSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTestSessions_WorkingMemoryTestId",
                table: "UserTestSessions",
                column: "WorkingMemoryTestId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingMemoryResponses_UserTestSessions_UserTestSessionId",
                table: "WorkingMemoryResponses",
                column: "UserTestSessionId",
                principalTable: "UserTestSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingMemoryResponses_UserTestSessions_UserTestSessionId",
                table: "WorkingMemoryResponses");

            migrationBuilder.DropTable(
                name: "UserTestSessions");

            migrationBuilder.DropColumn(
                name: "BlockNumber",
                table: "WorkingMemoryTerms");

            migrationBuilder.RenameColumn(
                name: "UserTestSessionId",
                table: "WorkingMemoryResponses",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingMemoryResponses_UserTestSessionId",
                table: "WorkingMemoryResponses",
                newName: "IX_WorkingMemoryResponses_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingMemoryResponses_Users_StudentId",
                table: "WorkingMemoryResponses",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
